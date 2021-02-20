using System;
using System.Collections.Generic;
using System.Text;

namespace MovieTime.Infrastructure.DTO
{
    public class UserUpdateDto : UserDto
    {

        public string Password { get; set; }

    }
}
