package com.sn.skidosnative;


import android.content.Context;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;
import android.database.sqlite.SQLiteOpenHelper;
import android.database.sqlite.SQLiteQueryBuilder;
import android.content.ContentValues;
import android.util.Log;

import java.sql.SQLException;

public class SkidosDataStore extends SQLiteOpenHelper {

    private static final String DATABASE_NAME = "SkidosDatabase.db";
    private static final String TABLE_NAME = "data";
    private static final String SQL_CREATE = "CREATE TABLE " + TABLE_NAME +
            " (id_key string PRIMARY KEY, value string)";

    private static final String SQL_DROP = "DROP TABLE IS EXISTS " + TABLE_NAME;

    SkidosDataStore(Context context) {
        super(context, DATABASE_NAME, null, 1);
    }

    @Override
    public void onCreate(SQLiteDatabase db) {
        db.execSQL(SQL_CREATE);
    }

    @Override
    public void onUpgrade(SQLiteDatabase db, int oldVersion, int newVersion) {
        db.execSQL(SQL_DROP);
        onCreate(db);
    }

    public Cursor getData(String id, String[] projection, String selection, String[] selectionArgs, String sortOrder) {
        SQLiteQueryBuilder sqliteQueryBuilder = new SQLiteQueryBuilder();
        sqliteQueryBuilder.setTables(TABLE_NAME);

        if(id != null) {
            sqliteQueryBuilder.appendWhere("id_key = '" + id + "'");
        }

        Cursor cursor = sqliteQueryBuilder.query(getReadableDatabase(),
                projection,
                selection,
                selectionArgs,
                null,
                null,
                sortOrder);
        // Log.d("Native Share" ,"Count :" +cursor.getCount());

        return cursor;
    }

    public long addNewData(ContentValues values) throws SQLException {
        long id = getWritableDatabase().insert(TABLE_NAME, "", values);
        if (id <= 0) {
            throw new SQLException("Failed to add data");
        }

        return id;
    }

    public int deleteData(String id) {
        if (id == null) {
            return getWritableDatabase().delete(TABLE_NAME, null, null);
        } else {
            return getWritableDatabase().delete(TABLE_NAME, "id_key=?", new String[]{id});
        }
    }

    public int updateData(String id, ContentValues values) {
        if (id == null) {
            return getWritableDatabase().update(TABLE_NAME, values, null, null);
        } else {
            return getWritableDatabase().update(TABLE_NAME, values, "id_key=?", new String[]{id});
        }
    }

}