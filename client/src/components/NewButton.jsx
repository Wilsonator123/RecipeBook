'use client'
import * as React from "react"
import { Button } from "@/components/ui/button"
import {
    Command,
    CommandGroup,
    CommandItem,
    CommandList,
} from "@/components/ui/command"
import {
    Popover,
    PopoverContent,
    PopoverTrigger,
} from "@/components/ui/popover"
import { Dialog, DialogTrigger } from "@/components/ui/dialog"
import NewItemPopup from "@/components/newItemPopup"

const frameworks = [
    {
        value: "website",
        label: "From Website",
        component: <NewItemPopup title="New Recipe From Website" description="Enter the URL of the website" inputName="URL"/>
    },
    {
        value: "video",
        label: "From Video",
        component: <NewItemPopup title="New Recipe From Video" description="Enter the URL of video" inputName="URL"/>
    },
    {
        value: "book",
        label: "From Book",
        component: <NewItemPopup title="New Recipe From Book" description="Upload images of the recipe" inputName="Images"/>
    },
    {
        value: "custom",
        label: "Custom",
        component: <NewItemPopup title="Enter recipe name" description="Add the name of your recipe" inputName="Reicpe Name"/>
    }
]

export function NewButton() {
    const [open, setOpen] = React.useState(false)
    const [dialogOpen, setDialogOpen] = React.useState(null)

    const handleSelect = (value) => {
        setOpen(false)
        setDialogOpen(value)
    }

    return (
        <>
            <Popover open={open} onOpenChange={setOpen}>
                <PopoverTrigger asChild>
                    <Button
                        variant="secondary"
                        role="combobox"
                        aria-expanded={open}
                        className="justify-between text-white text-lg"
                    >
                        New +
                    </Button>
                </PopoverTrigger>
                <PopoverContent className="w-[150px] p-0">
                    <Command>
                        <CommandList>
                            <CommandGroup>
                                {frameworks.map((framework) => (
                                    <CommandItem
                                        key={framework.value}
                                        value={framework.value}
                                        onSelect={() => handleSelect(framework.value)}
                                    >
                                        {framework.label}
                                    </CommandItem>
                                ))}
                            </CommandGroup>
                        </CommandList>
                    </Command>
                </PopoverContent>
            </Popover>
            {frameworks.map((framework) => (
                <Dialog
                    key={framework.value}
                    open={dialogOpen === framework.value}
                    onOpenChange={() => setDialogOpen(null)}
                >
                    <DialogTrigger asChild>
                        <div />
                    </DialogTrigger>
                    {framework.component}
                </Dialog>
            ))}
        </>
    )
}