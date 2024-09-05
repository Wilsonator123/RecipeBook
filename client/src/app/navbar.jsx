'use client'
import Logo from '../assets/logo.svg';
import Link from 'next/link';
import { usePathname, useRouter } from 'next/navigation';
import { Avatar, AvatarFallback, AvatarImage } from "@/components/ui/avatar"
import {
    DropdownMenu,
    DropdownMenuContent,
    DropdownMenuItem,
    DropdownMenuLabel,
    DropdownMenuSeparator,
    DropdownMenuTrigger,
} from "@/components/ui/dropdown-menu"



export default function Navbar() {
    const pathname = usePathname();
    const router = useRouter();

    const isActive = (active) => {
        console.log(pathname)
        return pathname === active
    }

    const isHome = () => {
        return pathname.startsWith('/app')
    }

    return (

        <div className="relative w-full flex items-center justify-between px-10 py-1 my-5">
            <Link href='/' className="flex items-center">
                <Logo />
                <h1 className="text-2xl mb-1">Recipe Book</h1>
            </Link>
            {isHome() &&
                <div className="flex justify-evenly items-center align-center rounded-3xl text-xl bg-white">
                    <Link href="/app" className={`${isActive('/app') && "bg-primary text-white underline"} rounded-3xl px-4 py-2 m-0`}>Home</Link>
                    <Link href="/app/recipes" className={`${isActive('/app/recipes') && "bg-primary text-white underline"} rounded-3xl px-4 py-2 m-0`}>Your Recipes</Link>
                    <Link href="/app/calendar" className={`${isActive('/app/calendar') && "bg-primary text-white underline"} rounded-3xl px-4 py-2 m-0`}>Calendar</Link>
                    <Link href="#" className={`${isActive('/app/ai') && "bg-primary text-white underline"} rounded-3xl px-4 py-2 m-0`}>Chat Bot</Link>
                </div>
            }
            <div className="flex gap-8 items-center">
                {false ?
                    <>
                        <Link href='/login' className="text-lg">Login</Link>
                        <Link href='/login?signup=true' className="text-lg bg-primary text-white rounded-3xl px-4 py-2">Sign up</Link>
                </>
                    :
                    <DropdownMenu>
                    <DropdownMenuTrigger className="flex items-center gap-3 focus:outline-none focus-visible:outline-none active:outline-none ring-0">
                            <h1>Hi, George!</h1>
                            <Avatar>
                                <AvatarImage src="https://github.com/shadcn.png" />
                                <AvatarFallback>CN</AvatarFallback>
                            </Avatar>
                        </DropdownMenuTrigger>
                        <DropdownMenuContent>
                            <DropdownMenuLabel>My Account</DropdownMenuLabel>
                            <DropdownMenuSeparator />
                            <DropdownMenuItem onClick={() => router.push('/account')}>Profile</DropdownMenuItem>
                            <DropdownMenuItem onClick={() => router.push('/account?settings')}>Settings</DropdownMenuItem>
                            <DropdownMenuItem>Log out</DropdownMenuItem>
                        </DropdownMenuContent>
                    </DropdownMenu>
                }

            </div>
        </div>
    )
}