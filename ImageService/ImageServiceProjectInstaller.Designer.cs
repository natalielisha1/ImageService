﻿namespace ImageService
{
    partial class ImageServiceProjectInstaller
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.imageServiceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.imageServiceInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // imageServiceProcessInstaller
            // 
            this.imageServiceProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.imageServiceProcessInstaller.Password = null;
            this.imageServiceProcessInstaller.Username = null;
            // 
            // imageServiceInstaller
            // 
            this.imageServiceInstaller.Description = "Image Folder Managing Service";
            this.imageServiceInstaller.DisplayName = "ImageService";
            this.imageServiceInstaller.ServiceName = "ImageService";
            this.imageServiceInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // ImageServiceProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.imageServiceProcessInstaller,
            this.imageServiceInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller imageServiceProcessInstaller;
        private System.ServiceProcess.ServiceInstaller imageServiceInstaller;
    }
}