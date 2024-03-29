﻿using E_CommerceForUdemy_DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceForUdemy_Business.Repository.IRepository
{
    public interface IUserRepository
    {
        Task<ApplicationUser> GetUserByEmail(string email);
        Task<ApplicationUser> GetUserByForgotPassword(string forgotPassword);
    }
}
