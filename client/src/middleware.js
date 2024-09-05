"use server";
import { NextResponse } from "next/server";

export async function middleware(request) {
    const url = request.nextUrl.clone()
    if (request.nextUrl.pathname === "/") {
        if (true) {
            return NextResponse.redirect(new URL("/home", url))
        } else {
            return NextResponse.redirect(new URL("/app", url))
        }
    }

    if (request.nextUrl.pathname.startsWith('/pp'))
    {
        return NextResponse.redirect(new URL('/login', url))
    }
}

export const config = {};