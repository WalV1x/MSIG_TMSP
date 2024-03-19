create table `Like`
(
    id       int auto_increment
        primary key,
    brand    varchar(255)   null,
    model    varchar(255)   null,
    price    decimal(10, 2) null,
    url      varchar(1000)  null,
    image    varchar(255)   null,
    date     datetime       null,
    users_id int            null
);

create index users_id_fk
    on `Like` (users_id);

INSERT INTO db_msig_liandro.`Like` (id, brand, model, price, url, image, date, users_id) VALUES (13, 'Nike', 'Dunk Low Cacao Wow', 135.00, 'https://wethenew.com/products/nike-dunk-low-cacao-wow', 'https://cdn.shopify.com/s/files/1/2358/2817/files/nike-dunk-low-wmns-cacao-wow_20628849_46151291_20482.png?v=1691768028', null, null);
INSERT INTO db_msig_liandro.`Like` (id, brand, model, price, url, image, date, users_id) VALUES (14, 'Nike', 'Dunk Low Black White', 115.00, 'https://wethenew.com/products/nike-dunk-low-black-white', 'https://cdn.shopify.com/s/files/1/2358/2817/products/dunk-low-black-white-822113.png?v=1638813882', null, null);
INSERT INTO db_msig_liandro.`Like` (id, brand, model, price, url, image, date, users_id) VALUES (15, 'New Balance', '1906D Protection Pack White Leather', 185.00, 'https://wethenew.com/products/new-balance-1906d-protection-pack-white-leather', 'https://cdn.shopify.com/s/files/1/2358/2817/files/new-balance-1906d-protection-pack-white-leather-1.png?v=1690897727', null, null);
INSERT INTO db_msig_liandro.`Like` (id, brand, model, price, url, image, date, users_id) VALUES (16, 'Adidas', 'Samba OG Black Wonder White', 155.00, 'https://wethenew.com/products/adidas-samba-og-black-wonder-white', 'https://cdn.shopify.com/s/files/1/2358/2817/files/adidas-samba-og-black-wonder-white2_d79085bc-49d3-42b6-a26c-babade7bb175.webp?v=1708680800', null, null);
INSERT INTO db_msig_liandro.`Like` (id, brand, model, price, url, image, date, users_id) VALUES (17, 'Nike', 'Nike SB Dunk Low Born X Raised One Block At A Time', 299.00, 'https://stockx.com/nike-sb-dunk-low-born-x-raised-one-block-at-a-time', 'https://images.stockx.com/images/Nike-SB-Dunk-Low-Born-x-Raised-One-Block-At-A-Time-Product.jpg?fit=', null, null);
INSERT INTO db_msig_liandro.`Like` (id, brand, model, price, url, image, date, users_id) VALUES (18, 'Adidas', 'Campus 00s Dark Green Cloud White (Enfant)', 115.00, 'https://wethenew.com/products/adidas-campus-00s-dark-green-cloud-white-enfant', 'https://cdn.shopify.com/s/files/1/2358/2817/files/adidas-campus-00s-dark-green-cloud-white-enfant-1.png?v=1707749059', '2024-03-08 00:00:00', null);
INSERT INTO db_msig_liandro.`Like` (id, brand, model, price, url, image, date, users_id) VALUES (19, 'Air Jordan', 'Air Jordan 4 Retro Frozen Moments', 325.00, 'https://wethenew.com/products/air-jordan-4-retro-frozen-moments', 'https://cdn.shopify.com/s/files/1/2358/2817/files/air-jordan-4-retro-frozen-moments-2.png?v=1704454075', '2024-03-08 00:00:00', null);
INSERT INTO db_msig_liandro.`Like` (id, brand, model, price, url, image, date, users_id) VALUES (20, 'Nike', 'Air Force 1 Low White Supreme', 185.00, 'https://wethenew.com/products/nike-air-force-1-low-white-supreme', 'https://cdn.shopify.com/s/files/1/2358/2817/products/air-force-1-low-white-supreme-799183.png?v=1638812513', '2024-03-08 00:00:00', null);
