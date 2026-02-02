-- Create Inventory Table
-- This table mirrors the fields from your local 'FoodItem.cs'
create table public.inventory (
  id uuid default gen_random_uuid() primary key,
  user_id uuid references auth.users not null, -- Links item to the logged-in user
  
  item_name text not null,
  item_type text,
  expiration_date date,
  item_price numeric,
  item_quantity integer default 1,
  storage_location text, -- Example: 'Freezer', 'Pantry', 'Dry Storage'
  item_unique_code text, -- Kept for compatibility with legacy code
  date_added date default timezone('utc'::text, now())::date,
  is_expired boolean default false,
  barcode text, -- [NEW] For the scanner integration
  
  created_at timestamp with time zone default timezone('utc'::text, now()) not null
);

-- Enable Row Level Security (RLS)
-- This ensures users can ONLY see their own items
alter table public.inventory enable row level security;

-- Create Security Policies
create policy "Users can view their own inventory"
on public.inventory for select
using ( auth.uid() = user_id );

create policy "Users can insert their own inventory"
on public.inventory for insert
with check ( auth.uid() = user_id );

create policy "Users can update their own inventory"
on public.inventory for update
using ( auth.uid() = user_id );

create policy "Users can delete their own inventory"
on public.inventory for delete
using ( auth.uid() = user_id );
