
namespace GraphVS
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
            this.btnPnlAdd = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnPnlRmv = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnPnlAdd
            // 
            this.btnPnlAdd.Location = new System.Drawing.Point(194, 82);
            this.btnPnlAdd.Name = "btnPnlAdd";
            this.btnPnlAdd.Size = new System.Drawing.Size(161, 23);
            this.btnPnlAdd.TabIndex = 0;
            this.btnPnlAdd.Text = "AddPanel";
            this.btnPnlAdd.UseVisualStyleBackColor = true;
            this.btnPnlAdd.Click += new System.EventHandler(this.btnAddPanel_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.panel1.Location = new System.Drawing.Point(406, 166);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 100);
            this.panel1.TabIndex = 1;
            // 
            // btnPnlRmv
            // 
            this.btnPnlRmv.Location = new System.Drawing.Point(194, 112);
            this.btnPnlRmv.Name = "btnPnlRmv";
            this.btnPnlRmv.Size = new System.Drawing.Size(161, 23);
            this.btnPnlRmv.TabIndex = 2;
            this.btnPnlRmv.Text = "Remove Furthest Panel";
            this.btnPnlRmv.UseVisualStyleBackColor = true;
            this.btnPnlRmv.Click += new System.EventHandler(this.btnPnlRmv_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnPnlRmv);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnPnlAdd);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnPnlAdd;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnPnlRmv;
    }
}

