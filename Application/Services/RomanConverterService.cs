﻿using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class RomanConverterService : IRomanConverterService
    {
        public string ToRoman(int number)
        {
            var map = new Dictionary<int, string> {
                {1000, "M"}, {900, "CM"}, {500, "D"}, {400, "CD"},
                {100, "C"}, {90, "XC"}, {50, "L"}, {40, "XL"},
                {10, "X"}, {9, "IX"}, {5, "V"}, {4, "IV"}, {1, "I"}
            };

            var result = new StringBuilder();
            foreach (var kv in map)
            {
                while (number >= kv.Key)
                {
                    result.Append(kv.Value);
                    number -= kv.Key;
                }
            }

            return result.ToString();
        }
    }
}
