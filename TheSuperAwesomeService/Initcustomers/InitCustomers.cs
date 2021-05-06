using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheSuperAwesomeService.Models;

namespace TheSuperAwesomeService.Initcustomers
{
    public static class InitCustomers
    {
        public static List<Customer> Init()
        {

            return new List<Customer>
            {
                new Customer(Guid.Parse("d9837922-4bff-4de0-bbec-f1af91a13498")){ 
                    Services = new List<IService>{ 
                        new ServiceA(new DateTime(2020,01,01)) }, 
                    Discounts= new List<Discount>{ new Discount { 
                        ServiceId="A", 
                        Start=new DateTime(2020,01,01), 
                        End=new DateTime(2020,02,01),
                        Percent=20 } }, 
                    FreeDays=0  },
                new Customer(Guid.NewGuid()){
                    Services = new List<IService>{
                        new ServiceA(new DateTime(2020,01,01)),
                        new ServiceB(new DateTime(2020,01,01)),
                    },
                    Discounts= new List<Discount>{ new Discount {
                        ServiceId="A",
                        Start=new DateTime(2020,01,01),
                        End=new DateTime(2020,02,01),
                        Percent=20 },
                     new Discount {
                        ServiceId="B",
                        Start=new DateTime(2020,01,01),
                        End=new DateTime(2020,02,01),
                        Percent=50 }},
                    FreeDays=0  }
            };


        }
    }
}
