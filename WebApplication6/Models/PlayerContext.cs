﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication6.Models
{
    public class PlayerContext:DbContext
    {
        public PlayerContext(DbContextOptions<PlayerContext> options) :base(options)
        {

        }

        public DbSet<Player>Players { get; set; }
    }
}
