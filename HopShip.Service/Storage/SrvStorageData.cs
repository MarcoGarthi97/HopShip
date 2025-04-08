﻿using HopShip.Data.DTO.Repository;
using HopShip.Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HopShip.Service.Storage
{
    public static class SrvStorageData
    {
        public static IEnumerable<MdlProduct> GetProducts()
        {
            IEnumerable<MdlProduct> products = new List<MdlProduct>
{
    new MdlProduct { Id = 1,  Name = "Prodotto 1",  Description = "Descrizione del prodotto 1",  Price = 10.00m,  Discount = 1.00m,  Stock = 101, Category = EnumCategoryProduct.Electronics,    IsActive = true, CreatedAt = new DateTime(2025, (1 % 12) + 1, (1 % 28) + 1) },
    new MdlProduct { Id = 2,  Name = "Prodotto 2",  Description = "Descrizione del prodotto 2",  Price = 20.00m,  Discount = 2.00m,  Stock = 102, Category = EnumCategoryProduct.Clothing,       IsActive = true, CreatedAt = new DateTime(2025, (2 % 12) + 1, (2 % 28) + 1) },
    new MdlProduct { Id = 3,  Name = "Prodotto 3",  Description = "Descrizione del prodotto 3",  Price = 30.00m,  Discount = 3.00m,  Stock = 103, Category = EnumCategoryProduct.HomeAppliances, IsActive = true, CreatedAt = new DateTime(2025, (3 % 12) + 1, (3 % 28) + 1) },
    new MdlProduct { Id = 4,  Name = "Prodotto 4",  Description = "Descrizione del prodotto 4",  Price = 40.00m,  Discount = 4.00m,  Stock = 104, Category = EnumCategoryProduct.Books,          IsActive = true, CreatedAt = new DateTime(2025, (4 % 12) + 1, (4 % 28) + 1) },
    new MdlProduct { Id = 5,  Name = "Prodotto 5",  Description = "Descrizione del prodotto 5",  Price = 50.00m,  Discount = 5.00m,  Stock = 105, Category = EnumCategoryProduct.Toys,           IsActive = true, CreatedAt = new DateTime(2025, (5 % 12) + 1, (5 % 28) + 1) },
    new MdlProduct { Id = 6,  Name = "Prodotto 6",  Description = "Descrizione del prodotto 6",  Price = 60.00m,  Discount = 6.00m,  Stock = 106, Category = EnumCategoryProduct.Electronics,    IsActive = true, CreatedAt = new DateTime(2025, (6 % 12) + 1, (6 % 28) + 1) },
    new MdlProduct { Id = 7,  Name = "Prodotto 7",  Description = "Descrizione del prodotto 7",  Price = 70.00m,  Discount = 7.00m,  Stock = 107, Category = EnumCategoryProduct.Clothing,       IsActive = true, CreatedAt = new DateTime(2025, (7 % 12) + 1, (7 % 28) + 1) },
    new MdlProduct { Id = 8,  Name = "Prodotto 8",  Description = "Descrizione del prodotto 8",  Price = 80.00m,  Discount = 8.00m,  Stock = 108, Category = EnumCategoryProduct.HomeAppliances, IsActive = true, CreatedAt = new DateTime(2025, (8 % 12) + 1, (8 % 28) + 1) },
    new MdlProduct { Id = 9,  Name = "Prodotto 9",  Description = "Descrizione del prodotto 9",  Price = 90.00m,  Discount = 9.00m,  Stock = 109, Category = EnumCategoryProduct.Books,          IsActive = true, CreatedAt = new DateTime(2025, (9 % 12) + 1, (9 % 28) + 1) },
    new MdlProduct { Id = 10, Name = "Prodotto 10", Description = "Descrizione del prodotto 10", Price = 100.00m, Discount = 10.00m, Stock = 110, Category = EnumCategoryProduct.Toys,           IsActive = true, CreatedAt = new DateTime(2025, (10 % 12) + 1, (10 % 28) + 1) },
    new MdlProduct { Id = 11, Name = "Prodotto 11", Description = "Descrizione del prodotto 11", Price = 110.00m, Discount = 11.00m, Stock = 111, Category = EnumCategoryProduct.Electronics,    IsActive = true, CreatedAt = new DateTime(2025, (11 % 12) + 1, (11 % 28) + 1) },
    new MdlProduct { Id = 12, Name = "Prodotto 12", Description = "Descrizione del prodotto 12", Price = 120.00m, Discount = 12.00m, Stock = 112, Category = EnumCategoryProduct.Clothing,       IsActive = true, CreatedAt = new DateTime(2025, (12 % 12) + 1, (12 % 28) + 1) },
    new MdlProduct { Id = 13, Name = "Prodotto 13", Description = "Descrizione del prodotto 13", Price = 130.00m, Discount = 13.00m, Stock = 113, Category = EnumCategoryProduct.HomeAppliances, IsActive = true, CreatedAt = new DateTime(2025, (13 % 12) + 1, (13 % 28) + 1) },
    new MdlProduct { Id = 14, Name = "Prodotto 14", Description = "Descrizione del prodotto 14", Price = 140.00m, Discount = 14.00m, Stock = 114, Category = EnumCategoryProduct.Books,          IsActive = true, CreatedAt = new DateTime(2025, (14 % 12) + 1, (14 % 28) + 1) },
    new MdlProduct { Id = 15, Name = "Prodotto 15", Description = "Descrizione del prodotto 15", Price = 150.00m, Discount = 15.00m, Stock = 115, Category = EnumCategoryProduct.Toys,           IsActive = true, CreatedAt = new DateTime(2025, (15 % 12) + 1, (15 % 28) + 1) },
    new MdlProduct { Id = 16, Name = "Prodotto 16", Description = "Descrizione del prodotto 16", Price = 160.00m, Discount = 16.00m, Stock = 116, Category = EnumCategoryProduct.Electronics,    IsActive = true, CreatedAt = new DateTime(2025, (16 % 12) + 1, (16 % 28) + 1) },
    new MdlProduct { Id = 17, Name = "Prodotto 17", Description = "Descrizione del prodotto 17", Price = 170.00m, Discount = 17.00m, Stock = 117, Category = EnumCategoryProduct.Clothing,       IsActive = true, CreatedAt = new DateTime(2025, (17 % 12) + 1, (17 % 28) + 1) },
    new MdlProduct { Id = 18, Name = "Prodotto 18", Description = "Descrizione del prodotto 18", Price = 180.00m, Discount = 18.00m, Stock = 118, Category = EnumCategoryProduct.HomeAppliances, IsActive = true, CreatedAt = new DateTime(2025, (18 % 12) + 1, (18 % 28) + 1) },
    new MdlProduct { Id = 19, Name = "Prodotto 19", Description = "Descrizione del prodotto 19", Price = 190.00m, Discount = 19.00m, Stock = 119, Category = EnumCategoryProduct.Books,          IsActive = true, CreatedAt = new DateTime(2025, (19 % 12) + 1, (19 % 28) + 1) },
    new MdlProduct { Id = 20, Name = "Prodotto 20", Description = "Descrizione del prodotto 20", Price = 200.00m, Discount = 20.00m, Stock = 120, Category = EnumCategoryProduct.Toys,           IsActive = true, CreatedAt = new DateTime(2025, (20 % 12) + 1, (20 % 28) + 1) },
    new MdlProduct { Id = 21, Name = "Prodotto 21", Description = "Descrizione del prodotto 21", Price = 210.00m, Discount = 21.00m, Stock = 121, Category = EnumCategoryProduct.Electronics,    IsActive = true, CreatedAt = new DateTime(2025, (21 % 12) + 1, (21 % 28) + 1) },
    new MdlProduct { Id = 22, Name = "Prodotto 22", Description = "Descrizione del prodotto 22", Price = 220.00m, Discount = 22.00m, Stock = 122, Category = EnumCategoryProduct.Clothing,       IsActive = true, CreatedAt = new DateTime(2025, (22 % 12) + 1, (22 % 28) + 1) },
    new MdlProduct { Id = 23, Name = "Prodotto 23", Description = "Descrizione del prodotto 23", Price = 230.00m, Discount = 23.00m, Stock = 123, Category = EnumCategoryProduct.HomeAppliances, IsActive = true, CreatedAt = new DateTime(2025, (23 % 12) + 1, (23 % 28) + 1) },
    new MdlProduct { Id = 24, Name = "Prodotto 24", Description = "Descrizione del prodotto 24", Price = 240.00m, Discount = 24.00m, Stock = 124, Category = EnumCategoryProduct.Books,          IsActive = true, CreatedAt = new DateTime(2025, (24 % 12) + 1, (24 % 28) + 1) },
    new MdlProduct { Id = 25, Name = "Prodotto 25", Description = "Descrizione del prodotto 25", Price = 250.00m, Discount = 25.00m, Stock = 125, Category = EnumCategoryProduct.Toys,           IsActive = true, CreatedAt = new DateTime(2025, (25 % 12) + 1, (25 % 28) + 1) },
    new MdlProduct { Id = 26, Name = "Prodotto 26", Description = "Descrizione del prodotto 26", Price = 260.00m, Discount = 26.00m, Stock = 126, Category = EnumCategoryProduct.Electronics,    IsActive = true, CreatedAt = new DateTime(2025, (26 % 12) + 1, (26 % 28) + 1) },
    new MdlProduct { Id = 27, Name = "Prodotto 27", Description = "Descrizione del prodotto 27", Price = 270.00m, Discount = 27.00m, Stock = 127, Category = EnumCategoryProduct.Clothing,       IsActive = true, CreatedAt = new DateTime(2025, (27 % 12) + 1, (27 % 28) + 1) },
    new MdlProduct { Id = 28, Name = "Prodotto 28", Description = "Descrizione del prodotto 28", Price = 280.00m, Discount = 28.00m, Stock = 128, Category = EnumCategoryProduct.HomeAppliances, IsActive = true, CreatedAt = new DateTime(2025, (28 % 12) + 1, (28 % 28) + 1) },
    new MdlProduct { Id = 29, Name = "Prodotto 29", Description = "Descrizione del prodotto 29", Price = 290.00m, Discount = 29.00m, Stock = 129, Category = EnumCategoryProduct.Books,          IsActive = true, CreatedAt = new DateTime(2025, (29 % 12) + 1, (29 % 28) + 1) },
    new MdlProduct { Id = 30, Name = "Prodotto 30", Description = "Descrizione del prodotto 30", Price = 300.00m, Discount = 30.00m, Stock = 130, Category = EnumCategoryProduct.Toys,           IsActive = true, CreatedAt = new DateTime(2025, (30 % 12) + 1, (30 % 28) + 1) },
    new MdlProduct { Id = 31, Name = "Prodotto 31", Description = "Descrizione del prodotto 31", Price = 310.00m, Discount = 31.00m, Stock = 131, Category = EnumCategoryProduct.Electronics,    IsActive = true, CreatedAt = new DateTime(2025, (31 % 12) + 1, (31 % 28) + 1) },
    new MdlProduct { Id = 32, Name = "Prodotto 32", Description = "Descrizione del prodotto 32", Price = 320.00m, Discount = 32.00m, Stock = 132, Category = EnumCategoryProduct.Clothing,       IsActive = true, CreatedAt = new DateTime(2025, (32 % 12) + 1, (32 % 28) + 1) },
    new MdlProduct { Id = 33, Name = "Prodotto 33", Description = "Descrizione del prodotto 33", Price = 330.00m, Discount = 33.00m, Stock = 133, Category = EnumCategoryProduct.HomeAppliances, IsActive = true, CreatedAt = new DateTime(2025, (33 % 12) + 1, (33 % 28) + 1) },
    new MdlProduct { Id = 34, Name = "Prodotto 34", Description = "Descrizione del prodotto 34", Price = 340.00m, Discount = 34.00m, Stock = 134, Category = EnumCategoryProduct.Books,          IsActive = true, CreatedAt = new DateTime(2025, (34 % 12) + 1, (34 % 28) + 1) },
    new MdlProduct { Id = 35, Name = "Prodotto 35", Description = "Descrizione del prodotto 35", Price = 350.00m, Discount = 35.00m, Stock = 135, Category = EnumCategoryProduct.Toys,           IsActive = true, CreatedAt = new DateTime(2025, (35 % 12) + 1, (35 % 28) + 1) },
    new MdlProduct { Id = 36, Name = "Prodotto 36", Description = "Descrizione del prodotto 36", Price = 360.00m, Discount = 36.00m, Stock = 136, Category = EnumCategoryProduct.Electronics,    IsActive = true, CreatedAt = new DateTime(2025, (36 % 12) + 1, (36 % 28) + 1) },
    new MdlProduct { Id = 37, Name = "Prodotto 37", Description = "Descrizione del prodotto 37", Price = 370.00m, Discount = 37.00m, Stock = 137, Category = EnumCategoryProduct.Clothing,       IsActive = true, CreatedAt = new DateTime(2025, (37 % 12) + 1, (37 % 28) + 1) },
    new MdlProduct { Id = 38, Name = "Prodotto 38", Description = "Descrizione del prodotto 38", Price = 380.00m, Discount = 38.00m, Stock = 138, Category = EnumCategoryProduct.HomeAppliances, IsActive = true, CreatedAt = new DateTime(2025, (38 % 12) + 1, (38 % 28) + 1) },
    new MdlProduct { Id = 39, Name = "Prodotto 39", Description = "Descrizione del prodotto 39", Price = 390.00m, Discount = 39.00m, Stock = 139, Category = EnumCategoryProduct.Books,          IsActive = true, CreatedAt = new DateTime(2025, (39 % 12) + 1, (39 % 28) + 1) },
    new MdlProduct { Id = 40, Name = "Prodotto 40", Description = "Descrizione del prodotto 40", Price = 400.00m, Discount = 40.00m, Stock = 140, Category = EnumCategoryProduct.Toys,           IsActive = true, CreatedAt = new DateTime(2025, (40 % 12) + 1, (40 % 28) + 1) },
    new MdlProduct { Id = 41, Name = "Prodotto 41", Description = "Descrizione del prodotto 41", Price = 410.00m, Discount = 41.00m, Stock = 141, Category = EnumCategoryProduct.Electronics,    IsActive = true, CreatedAt = new DateTime(2025, (41 % 12) + 1, (41 % 28) + 1) },
    new MdlProduct { Id = 42, Name = "Prodotto 42", Description = "Descrizione del prodotto 42", Price = 420.00m, Discount = 42.00m, Stock = 142, Category = EnumCategoryProduct.Clothing,       IsActive = true, CreatedAt = new DateTime(2025, (42 % 12) + 1, (42 % 28) + 1) },
    new MdlProduct { Id = 43, Name = "Prodotto 43", Description = "Descrizione del prodotto 43", Price = 430.00m, Discount = 43.00m, Stock = 143, Category = EnumCategoryProduct.HomeAppliances, IsActive = true, CreatedAt = new DateTime(2025, (43 % 12) + 1, (43 % 28) + 1) },
    new MdlProduct { Id = 44, Name = "Prodotto 44", Description = "Descrizione del prodotto 44", Price = 440.00m, Discount = 44.00m, Stock = 144, Category = EnumCategoryProduct.Books,          IsActive = true, CreatedAt = new DateTime(2025, (44 % 12) + 1, (44 % 28) + 1) },
    new MdlProduct { Id = 45, Name = "Prodotto 45", Description = "Descrizione del prodotto 45", Price = 450.00m, Discount = 45.00m, Stock = 145, Category = EnumCategoryProduct.Toys,           IsActive = true, CreatedAt = new DateTime(2025, (45 % 12) + 1, (45 % 28) + 1) },
    new MdlProduct { Id = 46, Name = "Prodotto 46", Description = "Descrizione del prodotto 46", Price = 460.00m, Discount = 46.00m, Stock = 146, Category = EnumCategoryProduct.Electronics,    IsActive = true, CreatedAt = new DateTime(2025, (46 % 12) + 1, (46 % 28) + 1) },
    new MdlProduct { Id = 47, Name = "Prodotto 47", Description = "Descrizione del prodotto 47", Price = 470.00m, Discount = 47.00m, Stock = 147, Category = EnumCategoryProduct.Clothing,       IsActive = true, CreatedAt = new DateTime(2025, (47 % 12) + 1, (47 % 28) + 1) },
    new MdlProduct { Id = 48, Name = "Prodotto 48", Description = "Descrizione del prodotto 48", Price = 480.00m, Discount = 48.00m, Stock = 148, Category = EnumCategoryProduct.HomeAppliances, IsActive = true, CreatedAt = new DateTime(2025, (48 % 12) + 1, (48 % 28) + 1) },
    new MdlProduct { Id = 49, Name = "Prodotto 49", Description = "Descrizione del prodotto 49", Price = 490.00m, Discount = 49.00m, Stock = 149, Category = EnumCategoryProduct.Books,          IsActive = true, CreatedAt = new DateTime(2025, (49 % 12) + 1, (49 % 28) + 1) },
    new MdlProduct { Id = 50, Name = "Prodotto 50", Description = "Descrizione del prodotto 50", Price = 500.00m, Discount = 50.00m, Stock = 150, Category = EnumCategoryProduct.Toys,           IsActive = true, CreatedAt = new DateTime(2025, (50 % 12) + 1, (50 % 28) + 1) },
    new MdlProduct { Id = 51, Name = "Prodotto 51", Description = "Descrizione del prodotto 51", Price = 510.00m, Discount = 51.00m, Stock = 151, Category = EnumCategoryProduct.Electronics,    IsActive = true, CreatedAt = new DateTime(2025, (51 % 12) + 1, (51 % 28) + 1) },
    new MdlProduct { Id = 52, Name = "Prodotto 52", Description = "Descrizione del prodotto 52", Price = 520.00m, Discount = 52.00m, Stock = 152, Category = EnumCategoryProduct.Clothing,       IsActive = true, CreatedAt = new DateTime(2025, (52 % 12) + 1, (52 % 28) + 1) },
    new MdlProduct { Id = 53, Name = "Prodotto 53", Description = "Descrizione del prodotto 53", Price = 530.00m, Discount = 53.00m, Stock = 153, Category = EnumCategoryProduct.HomeAppliances, IsActive = true, CreatedAt = new DateTime(2025, (53 % 12) + 1, (53 % 28) + 1) },
    new MdlProduct { Id = 54, Name = "Prodotto 54", Description = "Descrizione del prodotto 54", Price = 540.00m, Discount = 54.00m, Stock = 154, Category = EnumCategoryProduct.Books,          IsActive = true, CreatedAt = new DateTime(2025, (54 % 12) + 1, (54 % 28) + 1) }
};
            return products;
        }
    }
}
