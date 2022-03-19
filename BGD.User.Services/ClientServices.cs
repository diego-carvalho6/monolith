using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BGD.User.Entities;
using BGD.User.Entities.Extensions;
using BGD.User.Repository.Contracts;
using BGD.User.Services.Contracts;
using BGD.User.Services.Exceptions;

namespace BGD.User.Services
{
    public class ClientServices : IClientServices
    {
        private readonly IClientRepository _repository;
        public ClientServices(IClientRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Entities.Client>> GetAll() => await _repository.GetAsync();
        public async Task<object> Insert(Entities.Client client)
        {
            if (string.IsNullOrEmpty(client.Name))
            {
                throw new Exception("NOT_NULL_NAME");
            }
            var result = await _repository.InsertAsync(client);
            
            return client;
        }

        public async Task<Entities.Client> Get(Guid id)
        {
            var clientRepository = await _repository.FindAsync(id);

            if (clientRepository.Count() == 0)
            {
                throw new NotFoundException();
            }

            var client = clientRepository.FirstOrDefault();

            if (client.Orders == null)
            {
                client.Orders = new List<Order>();
            }
            
            var orders = await _repository.GetOrders(client);
            foreach (var order in orders)
            {
                client.Orders.Add(new Order
                {
                    Id = order.Id,
                    Createdat = order.Createdat,
                    Until = order.Until,
                    Discount = order.Discount,
                    Finalprice = order.Finalprice,
                    Finished = order.Finished,
                    Payed = order.Payed,
                    Progress = (Entities.Enums.OderStatus)order.Progress,
                });
            }
            
            
                
            return client;
        }

        public async Task<int> Delete(Guid id)
        {
            var clientRepository = await _repository.FindAsync(id);

            if (clientRepository.Count() == 0)
            {
                throw new NotFoundException();
            }

            return await _repository.DeleteAsync(id);
        }

        public async Task<Entities.Client> Put(Entities.Client client)
        {
            var clientRepository = await _repository.FindAsync(client.Id.Value);

            if (clientRepository.Count() == 0)
            {
                throw new NotFoundException();
            }
            
            return await _repository.UpdateAsync(client);
        }
    }
}