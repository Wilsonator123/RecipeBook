'use client'
import { useForm } from "react-hook-form"
import { Button } from "@/components/ui/button"
import {
    Form,
    FormControl,
    FormDescription,
    FormField,
    FormItem,
    FormLabel,
    FormMessage,
} from "@/components/ui/form"
import { Input } from "@/components/ui/input"
import Link from 'next/link'
import axios from "axios";
import {useRouter} from 'next/navigation'
import {updateUser} from "@/hooks/user";

export default function LoginForm()
{
    const router = useRouter();

    const form = useForm();

    const onSubmit = (data) => {
        axios
            .post("http://localhost:5000/user/login", data, {
                withCredentials: true,
            })
            .then(async (res) => {
                await updateUser();
                router.push("/app");
            })
            .catch((err) => {
                console.log(err);
            });
    };

    return (
        <Form {...form}>
            <form onSubmit={form.handleSubmit(onSubmit)} className="space-y-5 w-1/3 flex align-center mt-20 flex-col">
                <h1 className="!mb-[-10px] text-2xl">Sign in</h1>
                <FormField
                    control={form.control}
                    {...form.register("email", {required:true, pattern: /^((?!\.)[\w\-_.]*[^.])(@\w+)(\.\w+(\.\w+)?[^.\W])$/})}
                    name="email"
                    render={({field}) => (
                        <FormItem>
                            <FormLabel>Email</FormLabel>
                            <FormControl>
                                <Input  placeholder="Email" type="email" {...field} />
                            </FormControl>
                        </FormItem>
                    )}
                />
                <FormField
                    control={form.control}
                    {...form.register("password", {required:true})}
                    name="password"
                    render={({field}) => (
                        <FormItem>
                            <FormLabel>Password</FormLabel>
                            <FormControl>
                                <Input placeholder="Password" type="password" {...field} />
                            </FormControl>
                        </FormItem>
                    )}
                />
                <FormDescription className="underline text-sm float-right self-end !mt-3"><Link href="#">Forgot your
                    password?</Link></FormDescription>

                <Button className="text-white" variant="secondary" type="submit">Sign in</Button>
            </form>
        </Form>
    )
}