using AutoMapper;
using BusinessLayer.Dtos.Account;
using BusinessLayer.Dtos.Messages;
using BusinessLayer.Dtos.UserChat;
using BusinessLayer.Dtos.UserProfiles;
using BusinessLayer.Interfaces;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Security.Claims;
using Telegram.Bot.Types;

namespace BusinessLayer.Services
{
    public class UserMessageService : IUserMessageService
    {
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UserManager<UserProfile> _userManager;
        private readonly IUserChatRepository _userChatRepository;
        private readonly IUserMessageRepository _userMessageRepository;
        private readonly IAdvertRepository _advertRepository;
        private readonly IJobRepository _jobRepository;
        private readonly IFindRepository _findRepository;
        private static readonly SemaphoreSlim _dbContextSemaphore = new SemaphoreSlim(1);

        public UserMessageService(IMapper mapper, IHttpContextAccessor contextAccessor, UserManager<UserProfile> userManager, IUserMessageRepository userMessageRepository, IUserChatRepository userChatRepository, IAdvertRepository advertRepository, IJobRepository jobRepository, IFindRepository findRepository)
        {
            _mapper = mapper;
            _contextAccessor = contextAccessor;
            _userManager = userManager;
            _userMessageRepository = userMessageRepository;
            _userChatRepository = userChatRepository;
            _advertRepository = advertRepository;
            _jobRepository = jobRepository;
            _findRepository = findRepository;
        }

        public async Task<UserChatResponceDto> CreateChat(CreateChatRequestDto requestDto)
        {
            var userEmail = _contextAccessor.HttpContext?.User.Claims.Single(q => q.Type == ClaimTypes.Email).Value;
            var user = await _userManager.FindByEmailAsync(userEmail);
            var receiver = await _userManager.FindByNameAsync(requestDto.ReceiverUsername);
            if (requestDto.ChapterName == "Buy-Sell")
            {
                var advert = await _advertRepository.GetActiveById(requestDto.AdvertId);
                var searchedChat = await _userChatRepository.GetByUserIdsAndAdvertId(user.Id, receiver.Id, advert.Id);
                if (searchedChat == null)
                {
                    searchedChat = await _userChatRepository.Add(new UserChat
                    {
                        FirstUserId = user.Id,
                        SecondUserId = receiver.Id,
                        Id = Guid.NewGuid().ToString(),
                        AdvertId = requestDto.AdvertId,
                        ChapterName = requestDto.ChapterName
                    });
                    await _userChatRepository.Save();
                }
                return new UserChatResponceDto
                {
                    ChatId = searchedChat.Id,
                    NickName = advert.User.NickName,
                    Username = advert.User.UserName,
                    UserPhoto = advert.User.Photo,
                    AdvertPrice = advert.Price,
                    AdvertTitle = advert.Title,
                    AdvertPhoto = advert.Files.First().FileURL,
                    AdvertId = advert.Id,
                    ChapterName = requestDto.ChapterName
                };
            }
            else if (requestDto.ChapterName == "Services")
            {
                var job = await _jobRepository.GetActiveById(requestDto.AdvertId);
                var searchedChat = await _userChatRepository.GetByUserIdsAndAdvertId(user.Id, receiver.Id, job.Id);
                if (searchedChat == null)
                {
                    searchedChat = await _userChatRepository.Add(new UserChat
                    {
                        FirstUserId = user.Id,
                        SecondUserId = receiver.Id,
                        Id = Guid.NewGuid().ToString(),
                        AdvertId = requestDto.AdvertId,
                        ChapterName = requestDto.ChapterName
                    });
                    await _userChatRepository.Save();
                }
                return new UserChatResponceDto
                {
                    ChatId = searchedChat.Id,
                    NickName = job.User.NickName,
                    Username = job.User.UserName,
                    UserPhoto = job.User.Photo,
                    AdvertPrice = job.Price,
                    AdvertTitle = job.Title,
                    AdvertPhoto = job.Files.First().FileURL,
                    AdvertId = job.Id,
                    ChapterName = requestDto.ChapterName
                };
            }
            else
            {
                var find = await _findRepository.GetActiveById(requestDto.AdvertId);
                var searchedChat = await _userChatRepository.GetByUserIdsAndAdvertId(user.Id, receiver.Id, find.Id);
                if (searchedChat == null)
                {
                    searchedChat = await _userChatRepository.Add(new UserChat
                    {
                        FirstUserId = user.Id,
                        SecondUserId = receiver.Id,
                        Id = Guid.NewGuid().ToString(),
                        AdvertId = requestDto.AdvertId,
                        ChapterName = requestDto.ChapterName
                    });
                    await _userChatRepository.Save();
                }
                return new UserChatResponceDto
                {
                    ChatId = searchedChat.Id,
                    NickName = find.User.NickName,
                    Username = find.User.UserName,
                    UserPhoto = find.User.Photo,
                    AdvertTitle = find.Title,
                    AdvertPhoto = find.Files.First().FileURL,
                    AdvertId = find.Id,
                    ChapterName = requestDto.ChapterName
                };
            }
        }
        //В теории можно сделать, что если мы нажали на отправить сообщение и не написали ничего, то чат создался бы, но при следующей такой же попытке чат уже искался бы в бд и вовзращался бы с пустыми сообщениями
        //МОжно также перенести создание чата в CreateChat, а не в SendMessage
        //МОжно попробовать Join из LINQ. Типо сджоинить три коллекции с объявами

        public async Task<UserMessageResponceDto> SendMessage(SendMessageRequestDto requestDto)
        {
            var userEmail = _contextAccessor.HttpContext?.User.Claims.Single(q => q.Type == ClaimTypes.Email).Value;
            var user = await _userManager.FindByEmailAsync(userEmail);
            var receiver = await _userManager.FindByNameAsync(requestDto.ReceiverUsername);
            var searchedChat = await _userChatRepository.GetByUserIdsAndAdvertId(user.Id, receiver.Id, requestDto.AdvertId);
            var message = _mapper.Map<UserMessage>(requestDto);
            message.SenderId = user.Id;
            message.ReceiverId = receiver.Id;
            message.DateOfCreation = DateTime.UtcNow;
            message.Id = Guid.NewGuid().ToString();
            message.ChatId = searchedChat.Id;
            var addedUserMessage = await _userMessageRepository.Add(message);
            await _userMessageRepository.Save();
            return new UserMessageResponceDto
            {
                DateOfCreation = addedUserMessage.DateOfCreation,
                ReceiverUserName = receiver.UserName,
                SenderUserName = user.UserName,
                Text = addedUserMessage.Text
            };
        }
        public async Task<List<UserChatResponceDto>> GetChats()
        {
            var userEmail = _contextAccessor.HttpContext?.User.Claims.Single(q => q.Type == ClaimTypes.Email).Value;
            var user = await _userManager.FindByEmailAsync(userEmail);
            var chats = await _userChatRepository.GetByUserId(user.Id);

            if (chats.IsNullOrEmpty())
            {
                return new List<UserChatResponceDto>();
            }

            var chatDtos = await MapToUserChatResponceDtos(chats, userEmail);

            return chatDtos.ToList();
        }


        private async Task<UserChatResponceDto[]?> MapToUserChatResponceDtos(List<UserChat> chats, string userEmail)
        {
            return await Task.WhenAll(chats.Select(async chat =>
            {
                await _dbContextSemaphore.WaitAsync();
                try
                {
                    switch (chat.ChapterName)
                    {
                        case "Buy-Sell":
                            {
                                var advert = await _advertRepository.GetActiveById(chat.AdvertId);
                                return new UserChatResponceDto
                                {
                                    ChatId = chat.Id,
                                    Username = chat.FirstUser.Email == userEmail ? chat.SecondUser.UserName : chat.FirstUser.UserName,
                                    NickName = chat.FirstUser.Email == userEmail ? chat.SecondUser.NickName : chat.FirstUser.NickName,
                                    UserPhoto = chat.FirstUser.Email == userEmail ? chat.SecondUser.Photo : chat.FirstUser.Photo,
                                    AdvertId = advert.Id,
                                    AdvertTitle = advert.Title,
                                    AdvertPrice = advert.Price,
                                    AdvertPhoto = advert.MainPhoto,
                                    ChapterName = chat.ChapterName,
                                    SectionName = advert.Subsection.Name,
                                    SubsectionName = advert.Subsection.Section.Name
                                };
                            }
                        case "Services":
                            {
                                var job = await _jobRepository.GetActiveById(chat.AdvertId);
                                return new UserChatResponceDto
                                {
                                    ChatId = chat.Id,
                                    Username = chat.FirstUser.Email == userEmail ? chat.SecondUser.UserName : chat.FirstUser.UserName,
                                    NickName = chat.FirstUser.Email == userEmail ? chat.SecondUser.NickName : chat.FirstUser.NickName,
                                    UserPhoto = chat.FirstUser.Email == userEmail ? chat.SecondUser.Photo : chat.FirstUser.Photo,
                                    AdvertId = job.Id,
                                    AdvertTitle = job.Title,
                                    AdvertPrice = job.Price,
                                    AdvertPhoto = job.MainPhoto,
                                    ChapterName = chat.ChapterName,
                                    SectionName = job.Subsection.Name,
                                    SubsectionName = job.Subsection.Section.Name
                                };
                            }
                        case "Finds":
                            {
                                var find = await _findRepository.GetActiveById(chat.AdvertId);
                                return new UserChatResponceDto
                                {
                                    ChatId = chat.Id,
                                    Username = chat.FirstUser.Email == userEmail ? chat.SecondUser.UserName : chat.FirstUser.UserName,
                                    NickName = chat.FirstUser.Email == userEmail ? chat.SecondUser.NickName : chat.FirstUser.NickName,
                                    UserPhoto = chat.FirstUser.Email == userEmail ? chat.SecondUser.Photo : chat.FirstUser.Photo,
                                    AdvertId = find.Id,
                                    AdvertTitle = find.Title,
                                    AdvertPrice = string.Empty,
                                    AdvertPhoto = find.MainPhoto,
                                    ChapterName = chat.ChapterName,
                                    SectionName = find.Subsection.Name,
                                    SubsectionName = find.Subsection.Section.Name
                                };
                            }
                        default:
                            {
                                throw new InvalidOperationException("Invalid operation");
                            }
                    }
                }
                finally
                {
                    _dbContextSemaphore.Release();
                }
            }));
        }
        public async Task<List<GetChatMessageResponceDto>> GetChatMessages(string chatId)
        {
            var messages = await _userMessageRepository.GetByChatId(chatId);
            var responceDtos = _mapper.Map<List<GetChatMessageResponceDto>>(messages);
            return responceDtos;
        }
    }
}
