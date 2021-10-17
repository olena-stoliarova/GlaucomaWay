using System;
using System.ComponentModel.DataAnnotations;

namespace GlaucomaWay.Models
{
    public class Vf14ResultModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int UserId { get; set; }

        public DateTime Date { get; set; }

        [Required]
        [Range(0, 5)]
        public Vf14Answer Q1Score { get; set; }

        [Required]
        [Range(0, 5)]
        public Vf14Answer Q2Score { get; set; }

        [Required]
        [Range(0, 5)]
        public Vf14Answer Q3Score { get; set; }

        [Required]
        [Range(0, 5)]
        public Vf14Answer Q4Score { get; set; }

        [Required]
        [Range(0, 5)]
        public Vf14Answer Q5Score { get; set; }

        [Required]
        [Range(0, 5)]
        public Vf14Answer Q6Score { get; set; }

        [Required]
        [Range(0, 5)]
        public Vf14Answer Q7Score { get; set; }

        [Required]
        [Range(0, 5)]
        public Vf14Answer Q8Score { get; set; }

        [Required]
        [Range(0, 5)]
        public Vf14Answer Q9Score { get; set; }

        [Required]
        [Range(0, 5)]
        public Vf14Answer Q10Score { get; set; }

        [Required]
        [Range(0, 5)]
        public Vf14Answer Q11Score { get; set; }

        [Required]
        [Range(0, 5)]
        public Vf14Answer Q12Score { get; set; }

        [Required]
        [Range(0, 5)]
        public Vf14Answer Q13Score { get; set; }

        [Required]
        [Range(0, 5)]
        public Vf14Answer Q14Score { get; set; }

        public float Total { get; set; }
    }
}
