using System;
using System.ComponentModel.DataAnnotations;
using CampusHire.API.Entities;
namespace CampusHire.API.Models;

public class PasswordResetToken
{
    [Key]
    public int Id { get; set; }
    [Required]
    public int AdminId { get; set; }
    [Required]
    public string Token { get; set; } = string.Empty;
    [Required]
    public DateTime ExpiryTime { get; set; }
    public bool IsUsed { get; set; } = false;
    public Admin? Admin { get; set; }
}