using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Soundcloud_Clone.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddAlbumImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "Image",
                table: "Albums",
                type: "uuid",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Albums");
        }
    }
}
