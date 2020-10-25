using System;
using System.Collections.Generic;
using System.Text;
using ChuckNorris.Models;

namespace ChuckNorris.Services.DTO
{
    public class QueryDTO
    {
        public int total { get; set; }
        public IEnumerable<JokeDTO> result { get; set; }
    }
}
