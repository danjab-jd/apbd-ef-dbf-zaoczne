﻿namespace DbFirst.DTO
{
    public class BookDTO
    {
        public int IdBook { get; set; }
        public string Title { get; set; }
        public AuthorDTO Author { get; set; }
    }
}
