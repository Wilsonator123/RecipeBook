"use server";
import { cookies } from "next/headers";
export const getUser = async () => {
    "use server";
    try {
        const id = cookies().get("userid");
        if (!id) return false;

        const response = await fetch(
            process.env.API_URL + `/user/getUser`,
            {
                method: "GET",
                headers: {
                    "Content-Type": "application/json",
                    Cookie: "userid=" + id.value,
                },
                withCredentials: true,
                credentials: "include",
            }
        );
        return await response.json();
    } catch (error) {
        return false;
    }
};

export const clearUser = async () => {
    "use server";
    try {
        const id = cookies().get("userID");
        if (!id) return false;
        cookies().set("userid", "", { expires: new Date(0) });
    } catch (error) {
        return false;
    }
};