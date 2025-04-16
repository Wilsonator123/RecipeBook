"use server";

import { cookies } from 'next/headers';
import { NextResponse } from "next/server";

export async function middleware(request) {
    const url = request.nextUrl.clone()
    const user = await cookies().get('userid')

    if (request.nextUrl.pathname === "/") {
        if (user) {
            return NextResponse.redirect(new URL("/app", url))
        } else {
            return NextResponse.redirect(new URL("/home", url))
        }
    }

    if (request.nextUrl.pathname.startsWith('/app') && !user)
    {
        return NextResponse.redirect(new URL('/login', url))
    }
    
    if (request.nextUrl.pathname.startsWith('/login') && user)
    {
        return NextResponse.redirect(new URL('/app', url))
    }
}

export const config = {};