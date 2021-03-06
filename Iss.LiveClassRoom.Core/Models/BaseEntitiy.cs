﻿using Iss.LiveClassRoom.Core.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Iss.LiveClassRoom.Core.Models
{

    [SoftDelete(column: "IsDeleted", dateColumn: "TimeDeletedUtc")]
    public abstract class BaseEntity : IEntity {
        [Key, MaxLength(32)]
        public string Id { get; set; }
        [Required]
        public bool IsDeleted { get; set; }

        public DateTime TimeCreatedUtc { get; set; }
        public DateTime? TimeModifiedUtc { get; set; }
        public DateTime? TimeDeletedUtc { get; set; }

        [Required]
        public string CreatedByUserId { get; set; }
        public string ModifiedByUserId { get; set; }
        public string DeletedByUserId { get; set; }

        public BaseEntity() {
            Id = Guid.NewGuid().ToString("N");
        }

        public bool HasAccess(IPrincipal user, Permissions neededPermissions) {
            var compare = GetPermissions(user) & neededPermissions;
            return compare == neededPermissions;
        }

        public virtual Permissions GetPermissions(IPrincipal user) {
            return Permissions.Full; // EDIT: ToDo: ###
        }
    }
}
