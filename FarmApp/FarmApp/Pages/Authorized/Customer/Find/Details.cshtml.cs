﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FarmApp.Data;
using FarmApp.Models;
using Microsoft.AspNetCore.Identity;

namespace FarmApp.Pages.Authorized.Customer.Find
{
    public class DetailsModel : PageModel
    {
        private readonly FarmApp.Data.FarmAppContext _context;
        private readonly UserManager<User> _userManager;

        public DetailsModel(FarmApp.Data.FarmAppContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public Shop Shop { get; set; }
        public IList<Review> Review { get; set; }

        public int RatingAverage { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Shop = await _context.Shops.FirstOrDefaultAsync(m => m.Id == id);

            if (Shop == null)
            {
                return NotFound();
            }

            Review = await _context.Reviews
                    .Where(review => review.Shop.Id == id)
                    .OrderByDescending(review => review.CreateDate)
                    .Take(3)
                    .ToListAsync();

            calculateRatingAverages();

            if (Review.Any())
            {
                CutReviewText();
            }

            return Page();
        }

        private void CutReviewText()
        {
            int maxLength = 100;

            foreach (var currentReview in Review)
            {
                var currentComment = currentReview.Comment;

                if (currentComment.Length > maxLength)
                {
                    currentComment = currentComment.Substring(0, maxLength - 3) + "...";
                    currentReview.Comment = currentComment;
                }
            }
        }

        private void calculateRatingAverages()
        {
            RatingAverage = 0;

            if (Shop.Reviews != null && Shop.Reviews.Count > 0)
            {
                int sum = 0;

                foreach (var currentReview in Shop.Reviews)
                {
                    sum += currentReview.Rating;
                }

                RatingAverage = (int)((sum / (Shop.Reviews.Count * 5.0)) * 100);
            }
        }
    }
}
