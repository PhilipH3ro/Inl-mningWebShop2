using DC2.UI.Model;
using System.Collections.Generic;

namespace DC2.UI.Data
{
    public interface IDataOrder
    {
        void AddOrder(OrderDTO order);
        List<OrderDTO> GetAll();
        OrderDTO GetById(int id);
        void RemoveOrder(OrderDTO removeOrder);
        void UpdateOrder(OrderDTO newUserDetails);
    }
}