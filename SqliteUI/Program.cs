using DataAccessLibrary;
using DataAccessLibrary.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace SqliteUI
{
    class Program
    {
        static void Main(string[] args)
        {
            SqliteCrud sql = new SqliteCrud(GetConnectionString());

            //ReadAllContacts(sql);

            //ReadContact(sql, 1);

            //CreateNewContact(sql);
            //ReadAllContacts(sql);

            //UpdateContact(sql);
            //ReadAllContacts(sql);

            //DeletePhoneNumberFromContact(sql, 4, 1003);

            Console.WriteLine("Done Proccessing Sqlite");
            Console.ReadLine();
        }

        private static void DeletePhoneNumberFromContact(SqliteCrud sql, int contactId, int phoneNumberId)
        {
            sql.RemovePhoneNumberFromContact(contactId, phoneNumberId);
        }
        private static void UpdateContact(SqliteCrud sql)
        {
            BasicContactModel contact = new BasicContactModel
            {
                Id = 1,
                FirstName = "Abdelrahman",
                LastName = "Ahmed"
            };
            sql.UpdaateContactName(contact);
        }
        private static void CreateNewContact(SqliteCrud sql)
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
        private static void ReadAllContacts(SqliteCrud sql)
        {
            var rows = sql.GetAllContacts();

            foreach (var row in rows)
            {
                Console.WriteLine($"{row.Id}: {row.FirstName} {row.LastName}");
            }
        }
        private static void ReadContact(SqliteCrud sql, int contactId)
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
