package com.example.adaptationmapmobileapp.activities

import android.content.Intent
import android.os.Bundle
import android.widget.Button
import android.widget.EditText
import android.widget.Toast
import androidx.activity.enableEdgeToEdge
import androidx.appcompat.app.AppCompatActivity
import androidx.core.view.ViewCompat
import androidx.core.view.WindowInsetsCompat
import androidx.lifecycle.lifecycleScope
import com.example.adaptationmapmobileapp.R
import com.example.adaptationmapmobileapp.models.LoginModel
import com.example.adaptationmapmobileapp.services.AuthorizationService
import com.example.adaptationmapmobileapp.services.UserData
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch
import kotlinx.coroutines.withContext

class AuthorizationActivity : AppCompatActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        enableEdgeToEdge()
        setContentView(R.layout.activity_authorization)
        ViewCompat.setOnApplyWindowInsetsListener(findViewById(R.id.main)) { v, insets ->
            val systemBars = insets.getInsets(WindowInsetsCompat.Type.systemBars())
            v.setPadding(systemBars.left, systemBars.top, systemBars.right, systemBars.bottom)
            insets
        }

        val userData = UserData(this)
        if (userData.userId != 0) {
            val intent = Intent(this, MainActivity::class.java)
            startActivity(intent)
        }

        val loginEd: EditText = findViewById(R.id.login_ed)
        val passwordEd: EditText = findViewById(R.id.password_ed)
        val service = AuthorizationService()

        findViewById<Button>(R.id.login_button).setOnClickListener {
            lifecycleScope.launch {
                try {
                    val model = LoginModel(loginEd.text.toString(), passwordEd.text.toString())
                    userData.userId = withContext(Dispatchers.IO) { service.logIn(model)!!.id }
                    val intent = Intent(this@AuthorizationActivity, MainActivity::class.java)
                    startActivity(intent)
                } catch (exception: Exception) {
                    runOnUiThread {
                        Toast.makeText(
                            this@AuthorizationActivity,
                            "Ошибка: ${exception.message}",
                            Toast.LENGTH_SHORT
                        )
                            .show()
                    }
                }
            }
        }
    }
}