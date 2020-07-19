using DataLibrary.DataAccess;
using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataLibrary.BusinessLogic
{
    public static class CustomersProcessor
     {
        public static List<CustomerModel> LoadCustomers()
        {
            string sql = @"select Id, FirstName, LastName, EmailAddress, PhoneNumber, Address, UniqueCodeAccess, QuantityColected 
                            from dbo.Customer;";

            return SqlDataAccess.LoadData<CustomerModel>(sql);
        }
        public static int CreateCustomers(string firstName, string lastName, string emailAddress, string phoneNumber, string address, string uniqueCodeAccess)
        {
            CustomerModel data = new CustomerModel
            {
                FirstName = firstName,
                LastName = lastName,
                EmailAddress = emailAddress,
                UniqueCodeAccess=uniqueCodeAccess,
                PhoneNumber= phoneNumber,
                Address =address,
                QuantityColected=0
            };

            string sql = @"insert into dbo.Customer (FirstName, LastName, EmailAddress, PhoneNumber, Address, UniqueCodeAccess, QuantityColected)
                         values (@FirstName, @LastName, @EmailAddress, @PhoneNumber, @Address, @UniqueCodeAccess, @QuantityColected);";

            return SqlDataAccess.SaveData(sql, data);
        }

        public static int UpdateQuantityColected(int id, int Q, int x )
        {
            View data = new View
            {
                Id = id,
                x = Q+x
            };

            string sql = @"update dbo.Customer set QuantityColected= @x where Id = @Id;";
            return SqlDataAccess.UpdateData(sql, data);
        }

        public static int ActivateAccount(string emailAddress)
        {
            View data = new View
            {
                Data=emailAddress
            };
            
            string sql = @"update dbo.Customer set UniqueCodeAccess='Cont Activat' where EmailAddress = @Data;";
            return SqlDataAccess.UpdateData(sql, data);
        }

        public static int UpdateStatus(int Id, string Status)
        {
            View data = new View
            {
                Id = Id,
                Data = Status
            };

            string sql = @"update dbo.Location set Collected= @Data where OrderId = @Id;";
            return SqlDataAccess.UpdateData(sql, data);
        }

        public static List<WasteModel> LoadWaste()
        {
            string sql = @"select Id, TypeName from dbo.Waste;";

            return SqlDataAccess.LoadData<WasteModel>(sql);
        }

        public static int CreateWaste(string TypeName)
        {
            WasteModel data = new WasteModel
            {
                TypeName = TypeName,
            };

            string sql = @"insert into dbo.Waste (TypeName)
                         values (@TypeName);";

            return SqlDataAccess.SaveData(sql, data);
        }

        public static List<OrderModel> LoadComenzi()
        {
            string sql = @"select Id, Town, Address, Number, PhoneNumber, Date, CustomerId 
                            from dbo.Comanda;";

            return SqlDataAccess.LoadData<OrderModel>(sql);
        }
        public static int CreateComenzi(string town, string address, int number, string phoneNumber, DateTime date, int customerId)
        {
            OrderModel data = new OrderModel
            {
                Town = town,
                Address = address,
                Number = number,
                PhoneNumber = phoneNumber,
                Date = date,
                CustomerId = customerId
            };

            string sql = @"insert into dbo.Comanda (Town, Address, Number, PhoneNumber, Date, CustomerId)
                         values (@Town, @Address, @Number, @PhoneNumber, @Date, @CustomerId);";

            return SqlDataAccess.SaveData(sql, data);
        }

       

        public static List<LocationCoordinates> LoadLocations()
        {
            string sql = @"select OrderId, TypeId, Quantity, Address, Longitude, Latitude, Date, Collected, ClientName
                            from dbo.Location;";

            return SqlDataAccess.LoadData<LocationCoordinates>(sql);
        }
        public static int CreateLocations(int OrderId, int TypeId, int Quantity, string Address, string Longitude, string Latitude, DateTime Date, string ClientName)
        {
            LocationCoordinates data = new LocationCoordinates
            {
                OrderId = OrderId,
                TypeId = TypeId,
                Quantity = Quantity,
                Address = Address,
                Longitude = Longitude,
                Latitude = Latitude,
                Date = Date,
                Collected = "Fals",
                ClientName = ClientName
            };

            string sql = @"insert into dbo.Location (OrderId, TypeId, Quantity, Address, Longitude, Latitude, Date, Collected, ClientName)
                         values (@OrderId, @TypeId, @Quantity, @Address, @Longitude, @Latitude, @Date, @Collected, @ClientName);";

            return SqlDataAccess.SaveData(sql, data);
        }

        public static List<Reset> LoadReset()
        {
            string sql = @"select Id, ResetDate from dbo.Reset;";

            return SqlDataAccess.LoadData<Reset>(sql);
        }

        public static int CreateReset( DateTime ResetDate)
        {
            Reset data = new Reset
            { ResetDate = ResetDate };

            string sql = @"insert into dbo.Reset (ResetDate)
                         values (@ResetDate);";

            return SqlDataAccess.SaveData(sql, data);
        }
       
    }
}
