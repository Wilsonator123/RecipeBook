import Logo from '../assets/logo.svg';
import Link from 'next/link';

export default function Navbar() {

    return (

        <div className="relative w-full flex items-center justify-between px-10 py-1 my-5">
            <Link href='/' className="flex items-center">
                <Logo />
                <h1 className="text-2xl mb-1">Recipe Book</h1>
            </Link>
            <div className="flex justify-evenly items-center align-center rounded-3xl text-xl bg-white">
                <Link href="#" className={`${true && "bg-primary text-white underline"} rounded-3xl px-4 py-2 m-0`}>Home</Link>
                <Link href="#" className="rounded-3xl px-4 py-2 lh-normal">Recipes</Link>
                <Link href="#" className="rounded-3xl px-4 py-2">Contact</Link>
                <Link href="#" className="rounded-3xl px-4 py-2">About</Link>
            </div>
            <div className="flex gap-8 items-center">
                <Link href='/login' className="text-lg">Login</Link>
                <Link href='/login?signup=true' className="text-lg bg-primary text-white rounded-3xl px-4 py-2">Sign up</Link>
            </div>
        </div>
    )
}