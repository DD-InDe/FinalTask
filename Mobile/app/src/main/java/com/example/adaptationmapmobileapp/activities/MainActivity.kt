package com.example.adaptationmapmobileapp.activities

import android.os.Bundle
import android.widget.Toast
import androidx.activity.enableEdgeToEdge
import androidx.appcompat.app.AppCompatActivity
import androidx.core.view.ViewCompat
import androidx.core.view.WindowInsetsCompat
import androidx.lifecycle.lifecycleScope
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView
import com.example.adaptationmapmobileapp.R
import com.example.adaptationmapmobileapp.adapters.ModuleAdapter
import com.example.adaptationmapmobileapp.services.MapService
import com.example.adaptationmapmobileapp.services.UserData
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch
import kotlinx.coroutines.withContext

class MainActivity : AppCompatActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        enableEdgeToEdge()
        setContentView(R.layout.activity_main)
        ViewCompat.setOnApplyWindowInsetsListener(findViewById(R.id.main)) { v, insets ->
            val systemBars = insets.getInsets(WindowInsetsCompat.Type.systemBars())
            v.setPadding(systemBars.left, systemBars.top, systemBars.right, systemBars.bottom)
            insets
        }
    }

    override fun onResume() {
        super.onResume()

        val recyclerView = findViewById<RecyclerView>(R.id.modules_rv)

        lifecycleScope.launch {
            try {
                val service = MapService()
                val userData = UserData(this@MainActivity)
                val adaptationMap = withContext(Dispatchers.IO) { service.getMap(userData.userId) }
                runOnUiThread {
                    val adapter = ModuleAdapter(adaptationMap.modules, this@MainActivity)
                    recyclerView.adapter = adapter
                    recyclerView.layoutManager = LinearLayoutManager(this@MainActivity)
                }
            } catch (exception: Exception) {
                runOnUiThread {
                    Toast.makeText(this@MainActivity, "${exception.message}", Toast.LENGTH_SHORT)
                        .show()
                }
            }
        }
    }
}