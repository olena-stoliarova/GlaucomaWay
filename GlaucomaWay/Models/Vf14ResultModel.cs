using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace GlaucomaWay.Models;

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

    public Vf14ResultModel ToVf14ResultModel(PatientModel patient)
        => new ()
        {
            Date = Date,
            Q1Score = Q1Score,
            Q2Score = Q2Score,
            Q3Score = Q3Score,
            Q4Score = Q4Score,
            Q5Score = Q5Score,
            Q6Score = Q6Score,
            Q7Score = Q7Score,
            Q8Score = Q8Score,
            Q9Score = Q9Score,
            Q10Score = Q10Score,
            Q11Score = Q11Score,
            Q12Score = Q12Score,
            Q13Score = Q13Score,
            Q14Score = Q14Score,
            Total = CalculateTotal(),
            Patient = patient
        };

    private float CalculateTotal()
    {
        var scores = new Vf14Answer[] { Q1Score, Q2Score, Q3Score, Q4Score, Q5Score, Q6Score, Q7Score, Q8Score, Q9Score, Q10Score, Q11Score, Q12Score, Q13Score, Q14Score };
        var x4 = scores.Where(s => s == Vf14Answer.None).Count();
        var x4F = scores.Where(s => s == Vf14Answer.None).Count() * 4;
        var x3 = scores.Where(s => s == Vf14Answer.Alittle).Count();
        var x3F = scores.Where(s => s == Vf14Answer.Alittle).Count() * 3;
        var x2 = scores.Where(s => s == Vf14Answer.Moderate).Count();
        var x2F = scores.Where(s => s == Vf14Answer.Moderate).Count() * 2;
        var x1 = scores.Where(s => s == Vf14Answer.GreatDeal).Count();
        var x1F = scores.Where(s => s == Vf14Answer.GreatDeal).Count();

        float total = x4 + x3 + x2 + x1;
        float factored = x4F + x3F + x2F + x1F;

        return total != 0f ? factored / total : 0f;
    }
}