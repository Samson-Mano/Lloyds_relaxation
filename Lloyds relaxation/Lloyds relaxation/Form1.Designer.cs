namespace Lloyds_relaxation
{
    partial class Form1
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.main_pic = new System.Windows.Forms.Panel();
            this.mt_pic = new System.Windows.Forms.PictureBox();
            this.button_generate_surface = new System.Windows.Forms.Button();
            this.button_lloyds = new System.Windows.Forms.Button();
            this.main_pic.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mt_pic)).BeginInit();
            this.SuspendLayout();
            // 
            // main_pic
            // 
            this.main_pic.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.main_pic.BackColor = System.Drawing.Color.White;
            this.main_pic.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.main_pic.Controls.Add(this.mt_pic);
            this.main_pic.Location = new System.Drawing.Point(12, 58);
            this.main_pic.Margin = new System.Windows.Forms.Padding(3, 14, 3, 14);
            this.main_pic.Name = "main_pic";
            this.main_pic.Size = new System.Drawing.Size(804, 480);
            this.main_pic.TabIndex = 0;
            this.main_pic.SizeChanged += new System.EventHandler(this.main_pic_SizeChanged);
            this.main_pic.Paint += new System.Windows.Forms.PaintEventHandler(this.main_pic_Paint);
            // 
            // mt_pic
            // 
            this.mt_pic.BackColor = System.Drawing.Color.Transparent;
            this.mt_pic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mt_pic.Enabled = false;
            this.mt_pic.Location = new System.Drawing.Point(0, 0);
            this.mt_pic.Name = "mt_pic";
            this.mt_pic.Size = new System.Drawing.Size(800, 476);
            this.mt_pic.TabIndex = 0;
            this.mt_pic.TabStop = false;
            // 
            // button_generate_surface
            // 
            this.button_generate_surface.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_generate_surface.Location = new System.Drawing.Point(12, 12);
            this.button_generate_surface.Name = "button_generate_surface";
            this.button_generate_surface.Size = new System.Drawing.Size(126, 29);
            this.button_generate_surface.TabIndex = 1;
            this.button_generate_surface.Text = "Generate Voronoi";
            this.button_generate_surface.UseVisualStyleBackColor = true;
            this.button_generate_surface.Click += new System.EventHandler(this.button_generate_surface_Click);
            // 
            // button_lloyds
            // 
            this.button_lloyds.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_lloyds.Location = new System.Drawing.Point(164, 12);
            this.button_lloyds.Name = "button_lloyds";
            this.button_lloyds.Size = new System.Drawing.Size(129, 29);
            this.button_lloyds.TabIndex = 2;
            this.button_lloyds.Text = "Lloyds relaxation";
            this.button_lloyds.UseVisualStyleBackColor = true;
            this.button_lloyds.Click += new System.EventHandler(this.button_lloyds_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 61F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(828, 561);
            this.Controls.Add(this.button_lloyds);
            this.Controls.Add(this.button_generate_surface);
            this.Controls.Add(this.main_pic);
            this.Font = new System.Drawing.Font("Cambria Math", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 14, 3, 14);
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "Form1";
            this.Text = "Lloyd\'s Relaxation";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.main_pic.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mt_pic)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel main_pic;
        private System.Windows.Forms.Button button_generate_surface;
        private System.Windows.Forms.PictureBox mt_pic;
        private System.Windows.Forms.Button button_lloyds;
    }
}

