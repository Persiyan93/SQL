namespace FastFood.Core.ViewModels.Orders
{
    using FastFood.Models;
    using System.Collections.Generic;

    public class CreateOrderViewModel
    {
        public List<int> Items { get; set; }

        public string ItemName { get; set; }

        public string EmployeName { get; set; }

        public List<int> Employees { get; set; }
    }
}
