﻿using BusinessLayer.Interfaces;
using BusinessLayer.MapProfiles;
using BusinessLayer.Services;
using DataAccessLayer.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services, string connectionString, string vehiclesConnectionString)
        {
            services.AddEntityDependencies(connectionString, vehiclesConnectionString);
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IUserMessageService, UserMessageService>();
            services.AddScoped<IAdvertService, AdvertService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IAdvertFileService, AdvertFileService>();
            services.AddScoped<IEventService, EventService>();
            services.AddScoped<IUserThemeService, UserThemeService>();
            services.AddScoped<ICommonService, CommonService>();
            services.AddScoped<IJobService, JobService>();
            services.AddScoped<IJobFileService, JobFileService>();
            services.AddScoped<IJobFavouriteService, JobFavouriteService>();
            services.AddScoped<IAdvertFavouriteService, AdvertFavouriteService>();
            services.AddScoped<IFindService, FindService>();
            services.AddScoped<IFindCommentService, FindCommentService>();
            services.AddScoped<IFindFavouriteService, FindFavouriteService>();
            services.AddScoped<IFindFileService, FindFileService>();
            services.AddSingleton<TelegramBotService>();
            services.AddAutoMapper(typeof(UserMapProfile));
            return services;
        }
    }
}
