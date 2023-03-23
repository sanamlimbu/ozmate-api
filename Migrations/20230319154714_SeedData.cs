﻿using Microsoft.EntityFrameworkCore.Migrations;
using Bogus;
using OzMateApi.Entities;

#nullable disable

namespace OzMateApi.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var faker = new Faker();

            var users = Enumerable.Range(1, 10).Select(i => new User
            {
                Id = Guid.NewGuid(),
                Name = faker.Name.FullName(),
                Gender = faker.PickRandom<Gender>().ToString()
            }).ToList();

           foreach (var user in users)
            {
                migrationBuilder.InsertData(
                table: "Users",
                columns: new[] {"Id", "Name", "Gender" },
                values: new object[] {user.Id, user.Name, user.Gender }
                );

                var posts = Enumerable.Range(1, 5).Select(i => new Post
                {
                    Id = Guid.NewGuid(),
                    Content = faker.Lorem.Paragraph(),
                    UserId = user.Id
                }).ToList();

                foreach (var post in posts)
                {
                    migrationBuilder.InsertData(
                    table: "Posts",
                    columns: new[] { "Id", "Content", "UserId" },
                    values: new object[] { post.Id, post.Content, post.UserId }
                    );

                    var comments = Enumerable.Range(1, 5).Select(i => new Comment
                    {
                        Id = Guid.NewGuid(),
                        PostId = post.Id,
                        Content = faker.Lorem.Paragraph(),
                        UserId = post.UserId,
                    }).ToList();

                    foreach (var comment in comments)
                    {
                        migrationBuilder.InsertData(
                            table: "Comments",
                            columns: new[] { "Id", "Content", "PostId", "UserId" },
                            values: new object[] { comment.Id, comment.Content, comment.PostId, comment.UserId }
                            );
                    }
                }
            }  
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Comments");
            migrationBuilder.Sql("DELETE FROM Posts");
            migrationBuilder.Sql("DELETE FROM Users");
        }

        public enum Gender
        {
            Male,
            Female
        }
    }
}
