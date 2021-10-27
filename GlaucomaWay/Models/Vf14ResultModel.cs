using System;
using System.ComponentModel.DataAnnotations;

namespace GlaucomaWay.Models
{
    public class Vf14ResultModel
    {
        [Key]
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public Vf14Answer Q1Score { get; set; }

        public Vf14Answer Q2Score { get; set; }

        public Vf14Answer Q3Score { get; set; }

        public Vf14Answer Q4Score { get; set; }

        public Vf14Answer Q5Score { get; set; }

        public Vf14Answer Q6Score { get; set; }

        public Vf14Answer Q7Score { get; set; }

        public Vf14Answer Q8Score { get; set; }

        public Vf14Answer Q9Score { get; set; }

        public Vf14Answer Q10Score { get; set; }

        public Vf14Answer Q11Score { get; set; }

        public Vf14Answer Q12Score { get; set; }

        public Vf14Answer Q13Score { get; set; }

        public Vf14Answer Q14Score { get; set; }

        public float Total { get; set; }

        public PatientModel Patient { get; set; }
    }

    public class Vf14CreateOrUpdateModel
    {
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

        /// <summary>
        /// Total test score.
        /// </summary>
        /// <example>2</example>
        public float Total { get; set; }

        /// <summary>
        /// Parient Id.
        /// </summary>
        /// <example>1</example>
        [Range(1, int.MaxValue)]
        public int PatientId { get; set; }

        public Vf14ResultModel ToVf14ResultModel(PatientModel patient)
            => new ()
            {
                Date = Date,
                Q1Score = Q1Score,
                Q2Score = Q1Score,
                Q3Score = Q1Score,
                Q4Score = Q1Score,
                Q5Score = Q1Score,
                Q6Score = Q1Score,
                Q7Score = Q1Score,
                Q8Score = Q1Score,
                Q9Score = Q1Score,
                Q10Score = Q1Score,
                Q11Score = Q1Score,
                Q12Score = Q1Score,
                Q13Score = Q1Score,
                Q14Score = Q1Score,
                Total = Total,
                Patient = patient
            };
    }
}
