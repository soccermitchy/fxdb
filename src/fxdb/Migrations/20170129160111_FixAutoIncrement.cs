using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace fxdb.Migrations
{
    public partial class FixAutoIncrement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence<int>(
                name: "EffectIds");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "EffectItems",
                nullable: false,
                defaultValueSql: "NEXT VALUE FOR EffectIds",
                oldClrType: typeof(int))
                .Annotation("Sqlite:Autoincrement", true)
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.CreateIndex(
                name: "IX_EffectItems_Id",
                table: "EffectItems",
                column: "Id",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_EffectItems_Id",
                table: "EffectItems");

            migrationBuilder.DropSequence(
                name: "EffectIds");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "EffectItems",
                nullable: false,
                oldClrType: typeof(int),
                oldDefaultValueSql: "NEXT VALUE FOR EffectIds")
                .Annotation("Sqlite:Autoincrement", true)
                .OldAnnotation("Sqlite:Autoincrement", true);
        }
    }
}
