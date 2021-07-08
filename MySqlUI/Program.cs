using DataAccessLibrary;
using DataAccessLibrary.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace MySqlUI
{
    class Program
    {
        static void Main(string[] args)
        {
            MySqlCrud sql = new MySqlCrud(GetConnectionString());

            //ReadAllContacts(sql);

            //ReadContact(sql, 1);

            //CreateNewContact(sql);
            //ReadAllContacts(sql);

            //UpdateContact(sql);
            //ReadAllContacts(sql);

            //DeletePhoneNumberFromContact(sql, 4, 1003);

            Console.WriteLine("Done Proccessing MySql");
            Console.ReadLine();
        }

        private static void DeletePhoneNumberFromContact(MySqlCrud sql, int contactId, int phoneNumberId)
        {
            sql.RemovePhoneNumberFromContact(contactId, phoneNumberId);
        }
        private static void UpdateContact(MySqlCrud sql)
        {
            BasicContactModel contact = new BasicContactModel
            {
                Id = 1,
                FirstName = "Abdelrahman",
                LastName = "Ahmed"
            };
            sql.UpdaateContactName(contact);
        }
        private static void CreateNewContact(MySqlCrud sql)
        {
            FullContactModel user = new FullContactModel
            {
                BasicInfo = new BasicContactModel
                {
                    FirstName = "Kofi",
                    LastName = "Ahmed"
                }
            };
            user.EmailAddresses.Add(new EmailAddressModel
            {
                EmailAddress = "kofi@test.com"
            });
            user.EmailAddresses.Add(new EmailAddressModel
            {
                Id = 2,
                EmailAddress = "me@test.com"
            });

            user.PhoneNumbers.Add(new PhoneNumberModel
            {
                PhoneNumber = "123 - 113"
            });
            user.PhoneNumbers.Add(new PhoneNumberModel
            {
                Id = 2,
                PhoneNumber = "123 - 112"
            });

            sql.CreateContact(user);
        }
        private static void ReadAllContacts(MySqlCrud sql)
        {
            var rows = sql.GetAllContacts();

            foreach (var row in rows)
            {
                Console.WriteLine($"{row.Id}: {row.FirstName} {row.LastName}");
            }
        }
        private static void ReadContact(MySqlCrud sql, int contactId)
        {
            var contact = sql.GetFullContactById(contactId);

            Console.WriteLine($"{contact.BasicInfo.Id}: {contact.BasicInfo.FirstName} {contact.BasicInfo.LastName}");

        }
        private static string GetConnectionString(string connectionStringName = "Default")
        {
            string output = "";

            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");

            var config = builder.Build();

            output = config.GetConnectionString(connectionStringName);

            return output;
        }
    }
}
