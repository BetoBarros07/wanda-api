using System;
using System.ComponentModel.DataAnnotations;

namespace WHTW.Wanda.ViewModels.Schedule
{
    public class CreateSchedule
    {
        [Required(ErrorMessage = "Date is required.")]
        public DateTime Date { get; set; }
    }
}