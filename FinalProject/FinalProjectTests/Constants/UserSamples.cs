using Core.Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectTests.Constants
{
    public class UserSamples
    {
        public static UserForRegisterDto RegisterDefault => new UserForRegisterDto
        {
            Email = "s.canergenco@gmail.com",
            FirstName = "Sabri",
            LastName = "Genco",
            Password = "1234567"

        };

        public static UserForRegisterDto PasswordLenght4 => new UserForRegisterDto
        {
            Email = "s.canergenco@gmail.com",
            FirstName = "Sabri",
            LastName = "Genco",
            Password = "1234"

        };
        public static User DefaultUser => new User
        {
           Id =1,
            Email = "s.canergenco@gmail.com",
            FirstName = "Sabri",
            LastName = "Genco",
            PasswordHash = new byte[] { 1, 2, 3, 4, 5 },
            PasswordSalt = new byte[] { 6, 7, 8, 9, 10 },

            //  PasswordHash= Convert.FromHexString("0x58B107661B824BEDA0186FD6745A8D70E85F86617641E633C5B3AACB01A9531ADC6DA0CD22955DFD9D296264A9BBA5C53A5A28D82198F63C2917477BD492D84D"),
            //PasswordSalt= Convert.FromHexString("0x05430C66074A90F6EF22C931CF27AFAB4AA7B5BEB2559BF1E66204298F5A9010A56855C31047663DD7D7175C955845C4758B7B2AF80B86249CB15799E6E91A9D87BBBF71631F08A46806F96E001D9A1881BBE29104919CE1FFAB057236FA87F7DD780D6FE0D9EC30460B51C803FFEFD7658E1D314E28B5DE250EA436898F963B"),
            Status = true

        };
        public static UserForLoginDto LoginPasswordLenght4 => new UserForLoginDto
        {
            Email = "s.canergenco@gmail.com",
            Password = "1234"

        };

        public static UserForLoginDto LoginDefault => new UserForLoginDto
        {
            Email = "s.canergenco@gmail.com",
            Password = "1234567"

        };
    }
}
