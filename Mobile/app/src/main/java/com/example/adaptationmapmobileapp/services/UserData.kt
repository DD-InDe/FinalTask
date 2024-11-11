package com.example.adaptationmapmobileapp.services

import android.content.Context
import android.content.SharedPreferences

class UserData(context: Context) {
    private val prefs: SharedPreferences =
        context.getSharedPreferences("user_prefs", Context.MODE_PRIVATE)

    var userId: Int
        get() = prefs.getInt("user-id", 0)
        set(value) = prefs.edit().putInt("user-id", value).apply()
}