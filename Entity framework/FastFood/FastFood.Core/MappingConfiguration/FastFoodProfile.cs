namespace FastFood.Core.MappingConfiguration
{
    using AutoMapper;
    using FastFood.Core.ViewModels.Categories;
    using FastFood.Core.ViewModels.Employees;
    using FastFood.Core.ViewModels.Items;
    using FastFood.Core.ViewModels.Orders;
    using FastFood.Models;
    using FastFood.Models.Enums;
    using System;
    using ViewModels.Positions;

    public class FastFoodProfile : Profile
    {
        public FastFoodProfile()
        {
            //Positions
            
            this.CreateMap<CreatePositionInputModel, Position>()
                .ForMember(
                x => x.Name, 
                y => y.MapFrom(s => s.PositionName)
                );

            this.CreateMap<Position, PositionsAllViewModel>()
                .ForMember(x => x.Name, y => y.MapFrom(s => s.Name));


            //Category
            this.CreateMap<CreateCategoryInputModel, Category>()
                    .ForMember(x=>x.Name,
                                y=>y.MapFrom(z=>z.CategoryName));
            this.CreateMap<Category, CategoryAllViewModel>();


            ////Emploees

            this.CreateMap<Position, RegisterEmployeeViewModel>()
                .ForMember(x => x.PositionId,
                            y => y.MapFrom(z => z.Id))
                .ForMember(x => x.PositionName,
                            y => y.MapFrom(z => z.Name));


            this.CreateMap<RegisterEmployeeInputModel, Employee>()
                .ForMember(x => x.Position,
                              y => y.MapFrom(s => s.PositionName));

            this.CreateMap<Employee, EmployeesAllViewModel>()
                .ForMember(x => x.Position,
                           y => y.MapFrom(s => s.Position.Name));

            ////Items

            this.CreateMap<Category, CreateItemViewModel>()
                        .ForMember(x => x.CategoryId,
                                   y => y.MapFrom(z => z.Id))
                        .ForMember(x => x.CategoryName,
                                    y => y.MapFrom(z => z.Name));


            this.CreateMap<CreateItemInputModel, Item>()
                    .ForMember(x => x.CategoryId,
                                y => y.MapFrom(z => z.CategoryId));

            this.CreateMap<Item, ItemsAllViewModels>()
                    .ForMember(x => x.Category,
                               y => y.MapFrom(z => z.Category.Name));


            //Orders
            this.CreateMap<CreateOrderInputModel, Order>()
                        .ForMember(x => x.EmployeeId,
                                    y => y.MapFrom(z => z.EmployeeId))
                        .ForMember(x => x.DateTime,
                                    y => y.MapFrom(z => DateTime.UtcNow))
                        .ForMember(x => x.Type,
                                    y => y.MapFrom(z => OrderType.ToGo))
                        .ForMember(x => x.Customer,
                                    y => y.MapFrom(z => z.Customer));
                       
                

            this.CreateMap<CreateOrderInputModel, OrderItem>()
                .ForMember(x=>x.Quantity,
                            y=>y.MapFrom(z=>z.Quantity))
                .ForMember(x => x.ItemId,
                            y => y.MapFrom(z => z.ItemId));
                
                

            this.CreateMap<Employee,CreateOrderViewModel>()
                .ForMember(x=>x.EmployeName,
                            y=>y.MapFrom(z=>z.Name));
            this.CreateMap<Item, CreateOrderViewModel>()
                .ForMember(x => x.ItemName,
                            y => y.MapFrom(z => z.Name));
                        
               
                        
                        
                       
                        

                      





        }


    }
}
