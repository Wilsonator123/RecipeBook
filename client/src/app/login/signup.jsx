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
import {updateUser} from "@/hooks/user";
import {useRouter} from 'next/navigation'

export default function SignupForm()
{
    const form = useForm();
    const router = useRouter();


    const onSubmit = (data) => {
        axios
            .post("http://localhost:5000/user/createUser", data, {
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
                <h1 className="!mb-[-10px] text-2xl">Create an account</h1>
                <div className="flex w-full gap-5">
                    <FormField
                        control={form.control}
                        {...form.register("fname", {required:true, minLength: 2, maxLength: 50})}
                        name="fname"
                        render={({field}) => (
                            <FormItem className="w-1/2">
                                <FormLabel>First Name</FormLabel>
                                <FormControl>
                                    <Input className="w-full" placeholder="John" {...field} />
                                </FormControl>
                            </FormItem>
                        )}
                    />
                    <FormField
                        control={form.control}
                        {...form.register("lname", {required:true, minLength: 2, maxLength: 50})}
                        name="lname"
                        render={({field}) => (
                            <FormItem className="w-1/2">
                                <FormLabel>Last Name</FormLabel>
                                <FormControl>
                                    <Input className="w-full" placeholder="Smith" {...field} />
                                </FormControl>
                            </FormItem>
                        )}
                    />
                </div>
                <FormField
                    control={form.control}
                    {...form.register("email", {required:true, pattern: /^((?!\.)[\w\-_.]*[^.])(@\w+)(\.\w+(\.\w+)?[^.\W])$/})}
                    name="email"
                    render={({field}) => (
                        <FormItem>
                            <FormLabel>Email</FormLabel>
                            <FormControl>
                                <Input placeholder="Email" type="email" {...field} />
                            </FormControl>
                        </FormItem>
                    )}
                />
                <FormField
                    control={form.control}
                    {...form.register("password", {required:true, pattern: /.{8,64}/})}
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
                <FormField
                    control={form.control}
                    name="cpassword"
                    {...form.register("cpassword", {required:true, validate: {
                                equal: (value) =>
                                    value === form.getValues("password"),
                    }})}
                    render={({field}) => (
                        <FormItem>
                            <FormLabel>Confirm Password</FormLabel>
                            <FormControl>
                                <Input placeholder="Password" type="password" {...field} />
                            </FormControl>
                        </FormItem>
                    )}
                />

                <Button className="text-white" variant="secondary" type="submit">Sign up</Button>
            </form>
        </Form>
    )
}