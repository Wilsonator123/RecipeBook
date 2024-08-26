'use client'

import { useState } from 'react'
import LoginForm from './form'
import Google from '@/assets/Google.svg';
import Facebook from '@/assets/Facebook.svg'
import Link from 'next/link'
import SignupForm from "@/app/login/signup";

import { useSearchParams } from 'next/navigation'

export default function Page(){
    const searchParams = useSearchParams()


    //className="flex w-full min-h-[90vh]"
    const [login, setLogin] = useState(!searchParams.get("signup") ?? true);

    return (
        <div>
            <div className="flex flex-col w-full min-h-[70vh] justify-center items-center">
                { login ? <LoginForm /> : <SignupForm /> }
                <div className='flex flex-row w-1/3 items-center my-4'>
                    <hr className='border-[#909090] w-1/2'/>
                    <p className='text-[#909090] m-4'>Or</p>
                    <hr className='border-[#909090] w-1/2'/>
                </div>
                <div className="flex gap-5 mb-8">
                    <Link href="#" className="bg-secondary/40 rounded p-1">
                        <Google width={30} height={30} />
                    </Link>
                    <Link href="#" className="bg-secondary/40 rounded p-1">
                        <Facebook width={30} height={30} />
                    </Link>
                </div>
                {!login ?
                <div>Already have an account? <u onClick={() => setLogin(!login)}>Sign in</u></div>
                    :
                    <div> Don't have an account? <u onClick={() => setLogin(!login)}>Sign up</u></div>
                }
            </div>
        </div>
    )
}