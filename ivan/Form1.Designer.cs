namespace ivan
{
    partial class auth
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.phone_auth_txt = new System.Windows.Forms.TextBox();
            this.passport_auth_txt = new System.Windows.Forms.TextBox();
            this.auth_button = new System.Windows.Forms.Button();
            this.status_auth = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "phone number";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "passport";
            // 
            // phone_auth_txt
            // 
            this.phone_auth_txt.Location = new System.Drawing.Point(129, 12);
            this.phone_auth_txt.Name = "phone_auth_txt";
            this.phone_auth_txt.Size = new System.Drawing.Size(178, 20);
            this.phone_auth_txt.TabIndex = 2;
            // 
            // passport_auth_txt
            // 
            this.passport_auth_txt.Location = new System.Drawing.Point(129, 54);
            this.passport_auth_txt.Name = "passport_auth_txt";
            this.passport_auth_txt.Size = new System.Drawing.Size(178, 20);
            this.passport_auth_txt.TabIndex = 3;
            // 
            // auth_button
            // 
            this.auth_button.Location = new System.Drawing.Point(232, 99);
            this.auth_button.Name = "auth_button";
            this.auth_button.Size = new System.Drawing.Size(75, 23);
            this.auth_button.TabIndex = 4;
            this.auth_button.Text = "sign in";
            this.auth_button.UseVisualStyleBackColor = true;
            this.auth_button.Click += new System.EventHandler(this.auth_button_Click);
            // 
            // status_auth
            // 
            this.status_auth.FormattingEnabled = true;
            this.status_auth.Items.AddRange(new object[] {
            "customer",
            "employee"});
            this.status_auth.Location = new System.Drawing.Point(16, 99);
            this.status_auth.Name = "status_auth";
            this.status_auth.Size = new System.Drawing.Size(121, 21);
            this.status_auth.TabIndex = 5;
            // 
            // auth
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(317, 134);
            this.Controls.Add(this.status_auth);
            this.Controls.Add(this.auth_button);
            this.Controls.Add(this.passport_auth_txt);
            this.Controls.Add(this.phone_auth_txt);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "auth";
            this.Text = "authorize form";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox phone_auth_txt;
        private System.Windows.Forms.TextBox passport_auth_txt;
        private System.Windows.Forms.Button auth_button;
        private System.Windows.Forms.ComboBox status_auth;
    }
}

