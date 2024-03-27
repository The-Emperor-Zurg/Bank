using FluentMigrator;
using Itmo.Dev.Platform.Postgres.Migrations;

namespace Lab5.Infrastructure.DataAccess.Migrations;

[Migration(1, "Initial")]
public class Init : SqlMigration
{
    protected override string GetUpSql(IServiceProvider serviceProvider) =>
        """
        create table Users
        (
            user_name text not null UNIQUE,
            user_id bigint primary key generated always as identity,
            user_password text not null 
        );

        create table Cards
        (
            user_id bigint not null references Users(user_id),
            card_id bigint primary key generated always as identity,
            pin_code text not null,
            balance bigint not null
        );

        create table Transactions
        (
            card_id bigint not null references Cards(card_id),
            money numeric not null,
            date timestamp not null
        );
        """;

    protected override string GetDownSql(IServiceProvider serviceProvider) =>
        """
        drop table transactions;
        drop table balances;
        drop table users;
        """;
}