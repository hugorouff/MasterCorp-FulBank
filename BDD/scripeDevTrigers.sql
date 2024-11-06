# dev trigers:
use fulbank;


delimiter $$
    
/* working 
create trigger default_user_role before insert
	on Utilisateur for each row
    begin 
		if (new.typeProfile is null) then
        set new.typeProfile = 1;
        end if;
	end$$
# */ 

# not working
$$
drop trigger save_operation$$
create trigger save_operation before delete
on Opperation for each row
 begin
 /* impossible
	update Opperation 
    set suprimee = true
    where Opperation.id = old.id;
    */
    signal sqlstate '45000'
		set message_text = 'impossible de suprimer, passer le boolen de la variable suprimee à true';
        
	#call setTrue(old.id);	#non fonctionelle update impossible
 end$$


/* not posssible in mariadb, switch to postgres ?
create trigger soft_delete_view before delete
on Opperation for each row 
begin 
	update test set suprimee = true
    where test.id = old.id;
    signal sqlstate '45000'
		set message_text = 'soft delete done';
	end$$

create trigger solft_delete before delete
on test for each row
begin 
	update Opperation set suprimee = true
	where  Opperation.id = old.id;
    signal sqlState '45000'
		set message_text ='please work';
end $$
*/
/*drop procedure setTrue;
create procedure setTrue(in anid int)
 begin
    update Opperation
    set suprimee = true
    where Opperation.id = anid;	
 end$$
 */
 
 delimiter $$ 
drop function checkConnexion$$
create function checkConnexion(mdp varchar(255), usr int) returns bool
begin
declare mdp_check varchar(255) ;
select binary motDePasse into mdp_check from Utilisateur where id = usr;

if mdp_check = binary mdp 
then
return True;
else
return false;
end if;
end $$
delimiter ;