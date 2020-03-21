package com.sn.skidosnative;


import android.content.ContentProvider;
import android.content.ContentUris;
import android.content.ContentValues;
import android.content.UriMatcher;
import android.database.Cursor;
import android.net.Uri;
import android.content.Context;
import android.util.Log;


public class SkidosContentProvider extends ContentProvider {

    private static final String PROVIDER_NAME = "com.sn.skidosnative.SkidosContentProvider";
    protected static final Uri CONTENT_URI = Uri.parse("content://" + PROVIDER_NAME + "/data");
    private static final int DATA = 1;
    private static final int DATA_ID = 2;
    private static final UriMatcher uriMatcher = getUriMatcher();

    private SkidosDataStore skidosDataStore = null;


    private static UriMatcher getUriMatcher() {
        UriMatcher uriMatcher = new UriMatcher(UriMatcher.NO_MATCH);
        uriMatcher.addURI(PROVIDER_NAME, "data", DATA);
        uriMatcher.addURI(PROVIDER_NAME, "data/*", DATA_ID);
        return uriMatcher;
    }

    @Override
    public String getType(Uri uri) {
        Log.d("Native Share URI", uri.toString());
        switch (uriMatcher.match(uri)) {
            case DATA:
                return "vnd.android.cursor.dir/vnd.com.sn.skidoscp.SkidosContentProvider.data";

            case DATA_ID:
                return "vnd.android.cursor.item/vnd.com.sn.skidoscp.SkidosContentProvider.data";

        }
        return "";
    }

    @Override
    public boolean onCreate() {
        Context context = getContext();
        skidosDataStore = new SkidosDataStore(context);
        return true;
    }

    @Override
    public Cursor query(Uri uri, String[] projection, String selection, String[] selectionArgs, String sortOrder) {
        String id = null;
        final int match = uriMatcher.match(uri);
        Log.d("Native Share" ,"Query " + uri.toString());
        Log.d("Native Share" ,"Match " + match);
        switch (match) {
            case DATA:
                break;
            case DATA_ID:
                id = uri.getPathSegments().get(1);
                break;
            default:
                return null;
        }
        Log.d("Native Share" ,"Query " + (id != null ? id : "NULL"));

        return skidosDataStore.getData(id, projection, selection, selectionArgs, sortOrder);
    }

    @Override
    public Uri insert(Uri uri, ContentValues values) {
        try {
            if (uriMatcher.match(uri) == DATA) {
                long id = skidosDataStore.addNewData(values);
                return ContentUris.withAppendedId(CONTENT_URI, id);
            }
        } catch (Exception e) {
        }
        return null;
    }

    @Override
    public int delete(Uri uri, String selection, String[] selectionArgs) {
        String id = null;
        if (uriMatcher.match(uri) == DATA_ID) {
            id = uri.getPathSegments().get(1);
        }
        return skidosDataStore.deleteData(id);
    }

    @Override
    public int update(Uri uri, ContentValues values, String selection, String[] selectionArgs) {
        String id = null;
        if (uriMatcher.match(uri) == DATA_ID) {
            id = uri.getPathSegments().get(1);
        }

        return skidosDataStore.updateData(id, values);
    }
}