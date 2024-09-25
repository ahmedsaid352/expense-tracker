﻿namespace expense_tracker.Data.Models
{
    public class Expense
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Username { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
