﻿/**
 * Names: Ofek Segal & Natalie Elisha
 * IDs: 315638288 & 209475458
 * Exercise: Ex1
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.Threading.Tasks;

namespace ImageService
{
    [RunInstaller(true)]
    public partial class ImageServiceProjectInstaller : System.Configuration.Install.Installer
    {
        /// <summary>
        /// Constructor for ImageServiceProjectInstaller
        /// </summary>
        public ImageServiceProjectInstaller()
        {
            InitializeComponent();
        }
    }
}
