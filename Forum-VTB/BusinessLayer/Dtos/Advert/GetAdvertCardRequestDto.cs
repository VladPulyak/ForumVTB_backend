﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Dtos.Advert
{
    public class GetAdvertCardRequestDto
    {
        public string? AdvertId { get; set; }

        public string? UserId { get; set; }
    }
}
