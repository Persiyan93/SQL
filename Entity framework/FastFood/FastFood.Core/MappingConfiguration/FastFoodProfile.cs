namespace FastFood.Core.MappingConfiguration
{
    using AutoMapper;
    using FastFood.Core.ViewModels.Categories;
    using FastFood.Core.ViewModels.Employees;
    using FastFood.Core.ViewModels.Items;
    using FastFood.Core.ViewModels.Orders;
    using FastFood.Models;
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
                            y => y.MapFrom(z => z.Id));
            this.CreateMap<RegisterEmployeeInputModel, Employee>()
                .ForMember(x => x.Position,
                              y => y.MapFrom(s => s.PositionName));

            this.CreateMap<Employee, EmployeesAllViewModel>()
                .ForMember(x => x.Position,
                           y => y.MapFrom(s => s.Position.Name));

            ////Items

            this.CreateMap<Category, CreateItemViewModel>()
                        .ForMember(x=>x.CategoryId,
                                   y=>y.MapFrom(z=>z.Id));


            this.CreateMap<CreateItemInputModel, Item>()
                    .ForMember(x => x.CategoryId,
                                y => y.MapFrom(z => z.CategoryId));

            this.CreateMap<Item, ItemsAllViewModels>()
                    .ForMember(x => x.Category,
                               y => y.MapFrom(z => z.Category.Name));


            //Orders
            //this.CreateMap<CreateOrderViewModel,Order>()
            //            .ForMember(x=>x)





        }


    }
}
