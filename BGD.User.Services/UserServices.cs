using System;
using System.Collections;
using BGD.User.Repository.Contracts;
using BGD.User.Services.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;
using BGD.User.Services.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using BGD.User.Entities;
using BGD.User.Entities.Enums;
using BGD.User.Entities.Extensions;
using BGD.User.Services.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace BGD.User.Services
{
    public class UserServices : IUserServices
    {
        private readonly IUserRepository _repository;
        private readonly JWTServices _jwtServices;
        public UserServices(IUserRepository repository, JWTServices jwtServices)
        {
            _repository = repository;
            _jwtServices = jwtServices;
        }
        public async Task<IEnumerable<Entities.User>> GetAll() => await _repository.GetAsync();
        public async Task<Entities.User> Insert(Entities.User user, Entities.Tenant tenant = null)
        {
            if (tenant != null)
            {
                await _repository.InsertAdminAsync(user, tenant);
                return user;
            }
            var userRepository = await _repository.QueryAsync(user);

            if (userRepository.Count() != 0)
            {
                throw new AlreadyInUseException();
            }
            
            if (user?.Password.Length < 8 || user?.Password.Length > 30)
                throw new DigitPasswordException();

            // if (user.Status == UserStatus.Admin)
            // {
            //     user.Status = UserStatus.Staff;
            // }
            
            await _repository.InsertAsync(user);
            
            userRepository = await _repository.QueryAsync(user);
            FillpropertiesExtension.Fillproperties(user, userRepository);
            return user;
        }

        public async Task<object> Login(Entities.User user, Entities.Tenant tenant = null)
        {
            if (String.IsNullOrEmpty(user.Username) || String.IsNullOrEmpty(user.Password))
            {
                throw new IsNulOrEmptyException();
            }
            
            var _user = await _repository.QueryAsync(user, tenant);
            if (_user.Count() == 0)
            {
                throw new IncorrectUserNameException();
            }

            if (!_user.Select(x => x.Password?.Equals(user.Password)).FirstOrDefault())
            {
                throw new IncorrectPasswordException();
            }
         
            return new {Token = _jwtServices.GenerateTokenJWT(_user.FirstOrDefault(), tenant?.TenantName)};
        }

        public async Task<Entities.User> Get(Guid id)
        {
            var userRepository = await _repository.FindAsync(id);
            if (userRepository.Count() == 0)
            {
                throw new NotFoundException();
            }

            var user = userRepository.FirstOrDefault();

            user.Orders = new List<Order>();

            var userOrder = await _repository.QueryOrdersAsync(user.Id.Value);

            foreach (var order in userOrder)
            {
                var newOrder = new Order();
                
                FillpropertiesExtension.Fillproperties(newOrder, order);

                user.Orders.Add(newOrder);
            }
            
            return user;
        }

        public async Task<int> Delete(Guid id)
        {
            var user = await _repository.FindAsync(id); 
            if (user.Count() == 0)
            {
                throw new NotFoundException();
            }
            return await _repository.DeleteAsync(id);
        }

        public async Task<Entities.User> Put(Entities.User user)
        {
            if (user.Id == null)
            {
                throw new Exception("EMPTY_ID");
            }
            if (String.IsNullOrEmpty(user.Username) || String.IsNullOrEmpty(user.Password))
            {
                throw new IsNulOrEmptyException();
            }

            if (user.Password.Length < 8 || user.Password.Length > 30)
            {
                throw new DigitPasswordException();
            }
            
            var userRepository = await _repository.FindAsync(user.Id.Value); 
            if (userRepository.Count() == 0)
            {
                throw new NotFoundException();
            }

            if (userRepository.FirstOrDefault().Username != user.Username)
            {
                var userDynamic = await _repository.QueryAsync(user);
                if (userDynamic.Count() != 0)
                {
                    throw new AlreadyInUseException();
                }
            }

            if (userRepository.FirstOrDefault().Status != user.Status)
            {
                throw new SystemException("CAN_NOT_UPDATE_STATUS");
            }

            return await _repository.UpdateAsync(user);
        }

        public async Task<Entities.User> UpdateUserStatus(Entities.User user)
        {
            if (user.Id == null)
            {
                throw new Exception("EMPTY_ID");
            }
            if (String.IsNullOrEmpty(user.Username))
            {
                throw new IsNulOrEmptyException();
            }
            
            var userRepository = await _repository.FindAsync(user.Id.Value);

            if (user.Password != userRepository.FirstOrDefault().Password)
            {
                throw new System.Exception("WRONG_PASSWORD");
            }
            
            if (userRepository.FirstOrDefault().Username != user.Username)
            {
                throw new System.Exception("WRONG_USERNAME");
            }
            
            return await _repository.UpdateAsync(user);
        }
    }
}
