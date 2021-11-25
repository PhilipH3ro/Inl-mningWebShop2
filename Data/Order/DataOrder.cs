using Newtonsoft.Json;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DC2.UI.Model;

namespace DC2.UI.Data
{
    public class DataOrder : IDataOrder
    {
        public List<OrderDTO> GetAll()
        {
            var orders = LoadOrders();
            return (from cust in orders
                    orderby cust.OrderId
                    select cust).ToList();
        }

        public OrderDTO GetById(int id)
        {
            var orders = LoadOrders();
            return orders.FirstOrDefault(x => x.OrderId == id);
        }

        public void AddOrder(OrderDTO order)
        {
            var orders = LoadOrders();
            order.OrderId = GetNewId();
            orders.Add(order);

            SaveOrders(orders);
        }
        public void RemoveOrder(OrderDTO removeOrder)
        {
            var orders = LoadOrders();
            var order = orders.FirstOrDefault(x => x.OrderId == removeOrder.OrderId);
            orders.Remove(order);

            SaveOrders(orders);
        }
        public void UpdateOrder(OrderDTO newUserDetails)
        {
            var orders = LoadOrders();

            var order = orders.FirstOrDefault(x => x.OrderId == newUserDetails.OrderId);
            if (order is not null)
            {
                orders.Remove(order);
                orders.Add(newUserDetails);
            }
            orders = (from cust in orders
                      orderby order.OrderId
                      select cust).ToList();

            SaveOrders(orders);
        }

        private int GetNewId()
        {
            var orders = LoadOrders();
            var max = orders.Max(x => x.OrderId);
            return max + 1;
        }

        public void UpdatePayment(OrderDTO id, int status)
        {

        }

        public void SaveOrder(OrderDTO _object)
        {
            {
                List<OrderDTO> receipts = GetAll().ToList();

                if (receipts.Count() == 0)
                {
                    _object.OrderId = 0;
                }
                else
                {
                    _object.OrderId = (receipts.Max(x => x.OrderId) + 1);
                }
                receipts.Add(_object);
                File.WriteAllText(@"Json\Order.json", JsonConvert.SerializeObject(receipts));
            }
        }

        private List<OrderDTO> LoadOrders()
        {
            var path = @"Json\Order.json";
            var jsonResponse = File.ReadAllText(path);
            var tmp = JsonConvert.DeserializeObject<IEnumerable<OrderDTO>>(jsonResponse);
            return tmp.ToList();
        }

        private void SaveOrders(List<OrderDTO> orders)
        {
            var path = @"Json\Order.json";
            var jsonResponse = JsonConvert.SerializeObject(orders);
            File.WriteAllText(path, jsonResponse);
        }
    }
}
