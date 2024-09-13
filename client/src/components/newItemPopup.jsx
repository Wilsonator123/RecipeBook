'use client'
import { Button } from "@/components/ui/button"
import {
    DialogContent,
    DialogDescription,
    DialogFooter,
    DialogHeader,
    DialogTitle,
} from "@/components/ui/dialog"
import { Input } from "@/components/ui/input"
import { Label } from "@/components/ui/label"
import { recipeByWebsite } from "@/hooks/recipe"
import {useRouter} from "next/navigation";

export default function NewButton({ title, description, inputName }) {
    const router = useRouter()

    const onSubmit = async () => {
        const input = document.getElementById("input").value
        const response = await recipeByWebsite(input)

        if (response) {
            router.push(`/app/recipes/${response.Id}`)
        } else {
            alert("Recipe not found")
        }

    }

    return (
        <DialogContent className="sm:max-w-[425px]">
            <DialogHeader>
                <DialogTitle>{title}</DialogTitle>
                <DialogDescription>
                    {description}
                </DialogDescription>
            </DialogHeader>
            <div className="grid gap-4 py-4">
                <div className="grid grid-cols-4 items-center gap-4">
                    <Label htmlFor="username" className="text-right">
                        {inputName}
                    </Label>
                    <Input
                        id="input"
                        className="col-span-3"
                    />
                </div>
            </div>
            <DialogFooter>
                <Button type="submit" onClick={() => onSubmit()}>Generate Page</Button>
            </DialogFooter>
        </DialogContent>
    )
}