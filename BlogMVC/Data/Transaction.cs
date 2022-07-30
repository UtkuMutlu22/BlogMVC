using BlogMVC.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcenteWeb.Models
{
    public class Transaction:EntityBase
    {
        public Guid? ItemId { get; set; }
        public string EntityName { get; internal set; }
    }
    public class LikeTransaction : Transaction
    {
        public Guid? BlogId { get; set; }
        public Guid? CommentId { get; set; }

        public virtual Blog? Blog { get; set; }
        public virtual Comment? Comment { get; set; }
    }
    public class DisLikeTransaction : LikeTransaction
    {
    
    }
    public class FollowTransaction : Transaction
    {
    }
    public class UnFollowTransaction : Transaction
    {
    }

    public class TransactionEntityTypeConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
        }
    }
}
