using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using CarDealer.DTO;
using CarDealer.Models;

namespace CarDealer
{
    public class CarDealerProfile : Profile
    {
        public CarDealerProfile()
        {
            this.CreateMap<CarInputModel, Car>()
                .ForMember(c => c.Make, y => y.MapFrom(z => z.Make));
                
               


           
        
        }
    }
}
