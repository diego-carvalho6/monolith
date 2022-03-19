using System;
using System.Collections;
using System.Data;
using BGD.User.Entities;
using FluentMigrator;

namespace BGD.User.Repository.Dapper.Postgres.Migrations
{
    [Migration(202201110002)]
    public class UserTable_202201110002 : Migration
    {
        public override void Up()
        {
            // Create.Table("users_orders")
            //     .WithColumn("Id").AS

            Create.Table("user")
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("Name").AsString(100).Nullable()
                .WithColumn("Username").AsString(70).Nullable()
                .WithColumn("Password").AsString().Nullable()
                .WithColumn("Status").AsInt32().Nullable();

            // Create.Table("client")
            //     .WithColumn("Id").AsGuid().PrimaryKey()
            //     .WithColumn("Name").AsString(100).Nullable()
            //     .WithColumn("Description").AsString(100).Nullable()
            //     .WithColumn("Address").AsString().Nullable()
            //     .WithColumn("Cellphone").AsString(100).Nullable();

            Create.Table("order")
                .WithColumn("Id").AsGuid().PrimaryKey()
                // .WithColumn("Clientid").AsGuid().NotNullable().ForeignKey("client", "Id").OnDelete(Rule.Cascade)
                // .WithColumn("Setupdate").AsDateTime().NotNullable()
                .WithColumn("Finished").AsBoolean().NotNullable()
                .WithColumn("Payed").AsBoolean().NotNullable()
                .WithColumn("Finalprice").AsDecimal().NotNullable()
                .WithColumn("Discount").AsDecimal().NotNullable()
                .WithColumn("AdtionalFee").AsDecimal().NotNullable()
                .WithColumn("Createdat").AsDateTime().NotNullable()
                .WithColumn("Until").AsDateTime().NotNullable()
                .WithColumn("Table").AsInt32().NotNullable()
                .WithColumn("Description").AsString().NotNullable()
                .WithColumn("Progress").AsInt32().NotNullable();

            Create.Table("order_user")
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("UsersId").AsGuid()
                .WithColumn("OrdersId").AsGuid();
            
            Create.ForeignKey("Orders")
                .FromTable("order_user").ForeignColumn("UsersId")
                .ToTable("user").PrimaryColumn("Id").OnDeleteOrUpdate(Rule.Cascade);
            
            Create.ForeignKey("Employers")
                .FromTable("order_user").ForeignColumn("OrdersId")
                .ToTable("order").PrimaryColumn("Id").OnDeleteOrUpdate(Rule.Cascade);

            // Create.Table("to_do_list")
            //     .WithColumn("Id").AsGuid().PrimaryKey()
            //     .WithColumn("Orderid").AsGuid().NotNullable().ForeignKey("order", "Id").OnDelete(Rule.Cascade)
            //     .WithColumn("Comment").AsString().NotNullable()
            //     .WithColumn("Createdat").AsDateTime().NotNullable()
            //     .WithColumn("Username").AsString().NotNullable();

            Create.Table("pay_out")
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("Orderid").AsGuid().NotNullable().ForeignKey("order", "Id").OnDelete(Rule.Cascade)
                .WithColumn("Category").AsString().NotNullable()
                .WithColumn("Value").AsDecimal().NotNullable();


            Create.Table("item")
                .WithColumn("Id").AsGuid().PrimaryKey()
                // .WithColumn("Orderid").AsGuid().NotNullable().ForeignKey("order", "Id").OnDelete(Rule.Cascade)
                // .WithColumn("Width").AsDecimal().NotNullable()
                // .WithColumn("Height").AsDecimal().NotNullable()
                // .WithColumn("BuyValue").AsDecimal().NotNullable()
                // .WithColumn("Finished").AsBoolean().NotNullable()
                .WithColumn("Category").AsString().NotNullable()
                .WithColumn("Description").AsString(255).NotNullable()
                .WithColumn("Value").AsDecimal().NotNullable()
                .WithColumn("Cod").AsInt32().NotNullable()
                .WithColumn("Name").AsString().NotNullable();
            // .WithColumn("AdtionalFee").AsDecimal().NotNullable();

            Create.Table("order_item")
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("ItemsId").AsGuid()
                .WithColumn("OrdersId").AsGuid()
                .WithColumn("Status").AsBoolean()
                .WithColumn("Description").AsString();
            
            Create.ForeignKey("Orders")
                .FromTable("order_item").ForeignColumn("ItemsId")
                .ToTable("item").PrimaryColumn("Id").OnDeleteOrUpdate(Rule.Cascade);
            
            Create.ForeignKey("Items")
                .FromTable("order_item").ForeignColumn("OrdersId")
                .ToTable("order").PrimaryColumn("Id").OnDeleteOrUpdate(Rule.Cascade);

            // Create.Table("buy_value")
            //     .WithColumn("Id").AsGuid().PrimaryKey()
            //     .WithColumn("Meterprice").AsDecimal().Nullable()
            //     .WithColumn("Category").AsString().Nullable();

            
        }
        public override void Down()
        {
            // Delete.Table("buy_value");
            // Delete.Table("to_do_list");
            // Delete.Table("client");
            Delete.Table("order_item");
            Delete.Table("item");
            Delete.Table("pay_out");
            Delete.Table("order_user");
            Delete.Table("order");
            Delete.Table("user");
        }
    }
}
